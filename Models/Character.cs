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
        CurrentHp = maxHp; //초기 HP는 최대 HP로 설정
        CurrentMp = maxMp;
        AttackPower = attackPower;
        Defense = defense;
        Level = level;
    }

    #endregion

    #region 메서드

    

    #endregion
}