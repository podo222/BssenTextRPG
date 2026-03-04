using System;

namespace BssenTextRPG.Models;

public abstract class Item
{
    #region 프로퍼티
    
    //아이템 이름
    //protected set; : 외부에서 아이템 이름을 변경할 수 없도록 설정,
    //아이템 클래스 내부(혹은 상속받은 클래스)에서만 이름을 설정할 수 있도록 제한
    public string Name { get; protected set; }
    //아이템 설명
    public string Description { get; protected set; }
    //아이템 가격
    public int Price { get; protected set; }
    //아이템 타입
    public ItemType Type { get; protected set; }
    
    #endregion
    
    #region 생성자

    protected Item(string name, string description, int price, ItemType type)
    {
        Name = name;
        Description = description;
        Price = price;
        Type = type;
    }
    
    
    #endregion

    #region 메서드

    //아이템 사용 메서드 (추상 메서드)
    //아이템마다 효과가 다르므로, 아이템 클래스에서는 구체적인 구현을 하지 않고, 상속받은 클래스에서 구현하도록 강제
    //bool : 아이템이 사용가능한지 여부 반환
    //player마다 인벤토리를 따로 가지고 있어서 해당 플레이어가 가지고 있는 인벤토리에
    //해당 아이템이 있는지 여부를 판단하기 위해 Player 객체를 매개변수로 받음
    public abstract bool Use(Player player);
    
    //아이템 정보 표시 메서드
    public virtual void DisplayInfo()
    {
        Console.WriteLine($"[{Name}] {Description} (가격 : {Price} 골드)");
    }

    #endregion
}