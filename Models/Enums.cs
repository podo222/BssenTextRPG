namespace BssenTextRPG.Models;

public enum JobType
{
    Warrior,
    Archer,
    Wizard
    
}

//아이템 타입
public enum ItemType
{
    //무기류, 방어구, 포션
    Weapon,
    Armor,
    Potion
    //속성들을 추가할 아이템 스크립트 생성
}

//장비 슬롯 타입 (무기, 방어구, 악세사리)
public enum EquipmentSlot
{
    Weapon,
    Armor,
}