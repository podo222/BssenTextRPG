using System;

namespace BssenTextRPG.Models;

public class Enemy : Character
{
    #region 프로퍼티
    
    public int GoldReward { get; private set; }
    
    #endregion


    #region 생성자
    
    public Enemy(string name, int maxHp, int maxMp, int attackPower, int defense, int level, int goldReward) 
        : base(name, maxHp, maxMp, attackPower, defense, level)
    {
        GoldReward = goldReward;
    }
    
    #endregion

    #region 메서드

    //적 생성 메서드 (레벨에 따라 난이도 조절)
    public static Enemy CreateEnemy(int playerLevel)
    {
        //난수 생성기
        Random random = new Random();
        //적 레벨은 플레이어 레벨 ± 1 범위에서 랜덤하게 결정
        //random.Next(-1, 2) : -1, 0, 1 중 하나를 반환
        //Math.Max(1, ...) : 적 레벨이 최소 1이 되도록 보장
        int enemyLevel = Math.Max(1, playerLevel + random.Next(-1, 2)); //최소 레벨은 1
        //적 캐릭터의 종류
        string[] enemyTypes = { "고블린", "오크", "트롤" };
        string enemyName = enemyTypes[random.Next(0, enemyTypes.Length)]; //0, 3 : 0, 1, 2 중 하나를 반환
        
        //적 스텟 (레벨에 비례하여 증가)
        //enemyLevel - 1: 레벨 1에서는 기본 스텟, 레벨 2에서는 기본 스텟 + 증가량, 레벨 3에서는 기본 스텟 + 2배 증가량
        int maxHp = 50 + (enemyLevel - 1) * 20;
        int maxMp = 20 + (enemyLevel - 1) * 10;
        int attackPower = 20 + (enemyLevel - 1) * 5;
        int defense = 5 + (enemyLevel - 1) * 3;
        int goldReward = 20 + (enemyLevel - 1) * 10;
        
        return new Enemy($"Lv.{enemyLevel} {enemyName}", maxHp, maxMp, attackPower, defense, enemyLevel, goldReward);
    }
    
    //적 정보 출력

    public override void DisplayInfo()
    {
        Console.WriteLine($"===== {Name} =====");
        Console.WriteLine($"레벨 : {Level}");
        Console.WriteLine($"HP : {CurrentHp}/{MaxHp}");
        Console.WriteLine("공격력 : " + AttackPower);
        Console.WriteLine("방어력 : " + Defense);
    }
    
    public override int Attack(Character target)
    {
        // return target.TakeDamage(AttackPower);
        
        // 랜덤 공격력 부여
        // 일반 공격 (70% 확률) / 강한 공격(30% 확률)
        Random random = new Random();

        if (random.NextDouble() < 0.7)
        {
            // 일반 공격
            return target.TakeDamage(AttackPower);
        }
        else
        {
            // 강한 공격 (1.5배 데미지)
            Console.WriteLine($"{Name}의 강한 공격!");
            int damage = (int)(AttackPower * 1.5);
            return target.TakeDamage(damage);
        }
    }

    #endregion
}