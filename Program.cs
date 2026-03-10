using System;
using BssenTextRPG.Utils;
using BssenTextRPG.Data;
using BssenTextRPG.Systems;

namespace BssenTextRPG;

class Program
{
    static void Main(string[] args)
    {
        //콘솔 인코딩 설정 (한글 지원)
        Console.OutputEncoding = System.Text.Encoding.UTF8;
         
        //게임 저장 여부 확인
        if (SaveLoadSystem.IsSaveFileExsist())
        {
            // 메뉴 오픈 (새 게임, 이어하기, 종료)
            ShowStartMenu();
        }
        else
        {
            GameManager.Instance.StartGame();
        }
    }

    static void ShowStartMenu()
    {
        Console.Clear();
        ConsoleUI.ShowTitle();
        Console.WriteLine("\n╔════════════════════════════════╗");
        Console.WriteLine("║         게임 시작              ║");
        Console.WriteLine("╚════════════════════════════════╝");

        Console.WriteLine("\n1. 새 게임");
        Console.WriteLine("2. 이어하기");
        Console.WriteLine("0. 게임 종료");

        while (true)
        {
            Console.Write("선택> ");
            string? input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    //새 게임 시작
                    GameManager.Instance.StartGame();
                    return;
                case "2":
                    //이어하기
                    if (GameManager.Instance.LoadGame())
                    {
                        GameManager.Instance.StartGame(true);
                    }
                    return;
                case "0":
                    Console.WriteLine("게임을 종료합니다.");
                    return;
                default:
                    Console.WriteLine("잘못된 선택입니다. 다시 선택해주세요.");
                    return;
            }
        }
    }
}