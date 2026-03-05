using System;

namespace BssenTextRPG.Models;

//캐릭터 기본 추상 클래스
public abstract class Character
{
    #region 프로퍼티

    //protected : 해당 클래스 내에서는 접근 가능하지만, 외부에서는 접근 불가능한 접근 제한자, 자식 클래스에서는 접근 가능
    public string Name { get; protected set; }
    public int CurrentHp { get; protected set; }
    public int MaxHp { get; protected set; }
    public int CurrentMp { get; protected set; }
    public int MaxMp { get; protected set; }
    public int AttackPower { get; protected set; }
    public int Defense { get; protected set; }
    public int Level { get; protected set; }
    //생존 여부
    public bool IsAlive => CurrentHp > 0; //현재 HP가 0보다 크면 생존, 그렇지 않으면 사망

    #endregion

    #region 생성자

    protected Character(string name, int maxHp, int maxMp, int attackPower, int defense, int level)
    {
        Name = name;
        MaxHp = maxHp;
        MaxMp = maxMp;
        CurrentHp = maxHp; //초기 HP는 최대 HP로 설정
        CurrentMp = maxMp;
        AttackPower = attackPower;
        Defense = defense;
        Level = level;
    }

    #endregion

    #region 메서드

    //공통으로 사용할 메서드
    //추상 메서드(abstract method): 반드시 자식 메서드에서 구현해야 하는 메서드
    public abstract int Attack(Character target);
    
    //데미지 처리 메서드
    //가상 메서드(virtual method): 자식 클래스에서 필요에 따라 오버라이드하여 구현할 수 있는 메서드
    public virtual int TakeDamage(int damage)
    {
        // 방어력 적용
        int actualDamage = Math.Max(1, damage - Defense); //실제 데미지는 방어력을 뺀 값, 최소 1
        CurrentHp = Math.Max(0, CurrentHp - actualDamage);
        return actualDamage;
    }
    
    //캐릭터 스텟 출력
    public virtual void DisplayInfo()
    {
        Console.Clear();
        Console.WriteLine($"===== {Name} 정보 =====");
        Console.WriteLine($"레벨 : {Level}");
        Console.WriteLine($"체력 : {CurrentHp}/{MaxHp}");
        Console.WriteLine($"마나 : {CurrentMp}/{MaxMp}");
        Console.WriteLine($"공격력 : {AttackPower}");
        Console.WriteLine($"방어력 : {Defense}");
        Console.WriteLine("========================");
    }
    
    //HP 회복 메서드
    public int HealHp(int amount)
    {
        int beforeHp = CurrentHp;
        //회복 후 현재 HP가 최대 HP를 초과하지 않도록 설정
        CurrentHp = Math.Min(MaxHp, CurrentHp + amount);
        return CurrentHp - beforeHp; //실제 회복된 양을 반환
    }

    public int HealMp(int amount)
    {
        int beforeMp = CurrentMp;
        CurrentMp = Math.Min(MaxMp, CurrentMp + amount);
        return CurrentMp - beforeMp; //실제 회복된 양을 반환
    }
    
    //MP 회복 메서드

    #endregion
}