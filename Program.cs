using BssenTextRPG.Utils;
using BssenTextRPG.Data;

namespace BssenTextRPG;

class Program
{
    static void Main(string[] args)
    {
        //콘솔 인코딩 설정 (한글 지원)
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        
        //게임 저장 여부 확인
        //게임 로드 및 새 게임 시작
        
        GameManager.Instance.StartGame();
        
    }
}