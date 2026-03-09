using System;
using System.Text.Encodings.Web;
using System.Text.Json;
using BssenTextRPG.Data;
using BssenTextRPG.Models;

namespace BssenTextRPG.Systems;

public class SaveLoadSystem
{
    //저장 경로 및 파일명
    private const string SaveFilePath = "save.json";
    
    //JSON 직렬화 옵션
    //직렬화 의미 : 객체 -> 문자열
    //const와 readonly의 차이점 : const는 컴파일 타임에 값이 결정되고 변경할 수 없는 상수, readonly는 런타임에 값이 결정되며 한 번 할당된 후 변경할 수 없는 필드
    private static readonly JsonSerializerOptions jsonOptions = new()
    {
        WriteIndented = true, //가독성을 위한 들여쓰기
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping //한글 지원
    };

    #region 저장 기능

    public static bool SaveGame(Player player, InventorySystem inventory)
    {
        try
        {
            // 1. 게임 객체 (클래스) -> DTO(Data Transfer Object) 변환
            // json으로 변환하기 위한 클래스를 만들고, 그 클래스 안에 저장할 데이터를 담는다.
            // 필요한 데이터 : 플레이어 정보, 인벤토리 정보
            // 데이터 들을 모아서 PlayerData, ItemData로 만든다.
            var saveData = new GameSaveData
            {
                Player = ConvertToPlayerData(player),
                Inventory = ConvertToItemData(inventory)
            };
            
            //2. DTO -> JSON 문자열로 변환 (직렬화)
            string jsonString = JsonSerializer.Serialize(saveData, jsonOptions);
            
            //3. JSON 문자열을 파일로 저장
            File.WriteAllText(SaveFilePath, jsonString);
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }
    
    // Player 클래스를 PlayerData로 변환
    private static PlayerData ConvertToPlayerData(Player player)
    {
        return new PlayerData()
        {
            Name = player.Name,
            Job = player.Job.ToString(),
            Level = player.Level,
            CurrentHp = player.CurrentHp,
            MaxHp = player.MaxHp,
            CurrentMp = player.CurrentMp,
            MaxMp = player.MaxMp,
            AttackPower = player.AttackPower,
            Defense = player.Defense,
            Gold = player.Gold,
            EquipedWeaponName = player.EquipedWeapon?.Name,
            EquipedArmorName = player.EquipedArmor?.Name
        };
    }
    
    //Inventory를 ItemData로 변환
    private static List<ItemData> ConvertToItemData(InventorySystem inventory)
    {
        var itemDataList = new List<ItemData>();

        for (int i = 0; i < inventory.Count; i++)
        {
            var item = inventory.GetItem(i);
            if (item == null)
                continue;

            var itemData = new ItemData
            {
                Name = item.Name

            };

            if (item is Equipment equipment)
            {
                itemData.ItemType = "Equipment";
                itemData.Slot = equipment.Slot.ToString();
            }
            else if (item is Consumable consumable)
            {
                itemData.ItemType = "Consumable";
            }
            
            itemDataList.Add(itemData);
        }
        return itemDataList;
    }
    
    #endregion
}