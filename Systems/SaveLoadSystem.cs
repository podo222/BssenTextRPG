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

    #region 불러오기 기능
    //저장 파일 여부 확인
    public static bool IsSaveFileExsist()
    {
        return File.Exists(SaveFilePath);
    }

    public static GameSaveData? LoadGame()
    {
        try
        {
            //1. 저장된 JSON 파일에서 문자열 읽기
            string jsonString  = File.ReadAllText(SaveFilePath);
            Console.WriteLine(jsonString);
            
            //2. JSON 문자열 -> DTO 객체로 변환 (역직렬화)
            //JSON 문자열을 클래스 객체로 변환하는 과정 : 역직렬화
            var saveData = JsonSerializer.Deserialize<GameSaveData>(jsonString, jsonOptions);
            Console.WriteLine("\n게임데이터가 로드되었습니다.");
            return saveData;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }
    
    //PlayerData DTO를 Player 클래스로 변환하는 메서드
    public static Player LoadPlayer(PlayerData data)
    {
        //JobType을 문자열 -> 열거형 변환(Enum)
        var job = Enum.Parse<JobType>(data.Job);
        
        //Player 객체 생성
        var player = new Player(data.Name, job);
        
        //스텟 설정
        player.Level = data.Level;
        player.CurrentHp = data.CurrentHp;
        player.MaxHp = data.MaxHp;
        player.CurrentMp = data.CurrentMp;
        player.MaxMp = data.MaxMp;
        player.AttackPower = data.AttackPower;
        player.Defense = data.Defense;
        player.Gold = data.Gold;
        
        return player;
    }
    
    //ItemData DTO를 Inventory 클래스로 변환하는 메서드
    
    //저장된 장착 아이템을 복원하는 메서드 (무기/방어구)
    
    //아이템 생성 -> 인벤토리 추가
    #endregion
}