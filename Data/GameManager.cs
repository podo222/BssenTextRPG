using BssenTextRPG.Utils;

namespace BssenTextRPG.Data;

public class GameManager
{
    //Singleton Pattern 구현

    #region 싱글톤 패턴

    //싱글톤 인스턴스(내부 접근 용 변수: 필드)
    private static GameManager instance;
    
    //외부에서 인스턴스에 접근하기 위한 프로퍼티
    public static GameManager Instance
    {
        get
        {
            //인스턴스가 아직 생성되지 않았다면 새로 생성
            if (instance == null)
            {
                instance = new GameManager();
            }

            return instance;
        }
    }

    private GameManager()
    {
        //생성자: 클래스가 생성될 때 초기화 작업 수행
    }
    #endregion

    #region 게임시작/종료

    // 게임 시작 메서드
    public void StartGame()
    {
        ConsoleUI.ShowTitle();
        Console.WriteLine("빡센 게임에 오신 것을 환영합니다!\n");
    }

    #endregion
    
    
}