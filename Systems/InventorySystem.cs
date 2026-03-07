using System;
using System.Collections.Generic;
using BssenTextRPG.Models;
using BssenTextRPG.Utils;

namespace BssenTextRPG.Systems;

public class InventorySystem
{
    #region 프로퍼티
    //아이템 목록
    private List<Item> Items { get; set; }
    
    //아이템 갯수 (읽기 전용, 쓰기X)
    public int Count => Items.Count; //goes to
    //public int Count { get { return Items.Count; }}

    #endregion

    #region 생성자

    public InventorySystem()
    {
        Items = new List<Item>();
    }

    #endregion

    #region 아이템 관리

    //아이템 추가
    public void AddItem(Item item)
    {
        Items.Add(item);
        Console.WriteLine($"{item.Name}을 인텐토리에 추가했습니다.");
    }
    //아이템 삭제
    public bool RemoveItem(Item item)
    {
        if (Items.Remove(item))
        {
            Console.WriteLine($"{item.Name}을 인벤토리에서 제거했습니다.");
            return true;
        }

        return false;
    }

    //인덱스 값으로 아이템 반환
    public Item? GetItem(int index)
    {
        if (index >= 0 && index < Items.Count)
        {
            return Items[index];
        }

        return null;
    }
    #endregion

    #region 인벤토리 표시
    public void DisplayInventory()
    {
        Console.Clear();
        Console.WriteLine("\n╔════════════════════════════════╗");
        Console.WriteLine("║         인벤토리               ║");
        Console.WriteLine("╚════════════════════════════════╝");

        if (Items.Count == 0)
        {
            Console.WriteLine("인벤토리가 비어있습니다.");
            return;
        }

        Console.WriteLine("\n[보유 아이템]");
        for (int i = 0; i < Items.Count; i++)
        {
            Console.Write($"[{i + 1}] ");
            Items[i].DisplayInfo();
        }
    }

    public void ShowInventoryMenu(Player player)
    {
        while (true)
        {
            DisplayInventory();

            Console.WriteLine("\n선택하세요.");
            Console.WriteLine("1. 아이템 사용");
            Console.WriteLine("2. 아이템 버리기");
            Console.WriteLine("0. 나가기");
            Console.Write("선택: ");
            
            string? input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    //아이템 사용 로직
                    UseItem(player);
                    break;
                case "2":
                    //아이템 버리기 로직
                    DropItem(player);
                    break;
                case "0":
                    return; //인벤토리 메뉴 종료
                default:
                    Console.WriteLine("잘못된 선택입니다. 다시 선택하세요.");
                    break;
            }
        }
    }
    #endregion

    #region 아이템 사용

    private void UseItem(Player player)
    {
        if (Items.Count == 0)
        {
            Console.WriteLine("인벤토리가 비어있습니다.");
            return;
        }

        Console.Write("\n사용할 아이템 번호 (0 : 취소)> ");
        
        //int.TryParse() : 문자열을 정수로 변환하려고 시도하는 메서드, 변환이 성공하면 true 반환, 실패하면 false 반환
        if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= Items.Count)
        {
            Item item = Items[index - 1];
            if (item.Use(player))
            {
                //소모품일 경우 사용 후 리스트에서 제거함
                if (item is Consumable)
                {
                    RemoveItem(item);
                }
            }
        }
        else if (index != 0)
        {
            Console.WriteLine("잘못된 선택입니다.");
            ConsoleUI.PressAnyKey();
        }
    }


    #endregion

    #region 아이템 버리기

    private void DropItem(Player player)
    {
        if (Items.Count == 0) return;

        Console.WriteLine("\n버릴 아이템 번호 (0 : 취소)> ");

        if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= Items.Count)
        {
            Item item = Items[index - 1];
            Console.Write($"정말 {item.Name}을 버리겠습니까? (Y/N) > ");
            //ToLower() : 문자열을 소문자로 변환하는 메서드, 사용자가 대소문자 구분 없이 입력할 수 있도록 함
            if (Console.ReadLine()?.ToLower() == "y")
            {
                //장착 해제 로직
                //item이 Equipment 타입인지 확인하고 equipment 변수에 할당
                if (item is Equipment equipment)
                {
                    if (equipment == player.EquipedWeapon)
                    {
                        player.UnequipItem(EquipmentSlot.Weapon);
                    }
                    else if (equipment == player.EquipedArmor)
                    {
                        player.UnequipItem(EquipmentSlot.Armor);
                    }
                }
                
                RemoveItem(item);

                Console.WriteLine($"{item.Name}을 버렸습니다.");
                ConsoleUI.PressAnyKey();
            }
            else if (index != 0)
            {
                Console.WriteLine("잘못된 선택입니다.");
                ConsoleUI.PressAnyKey();
            }
        }
    }

    #endregion
}