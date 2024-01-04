namespace homework
{
    internal class Program
    {
        static void Print(int _turn, string[] _tictactoe)
        {
            Console.Clear();//시작 전 이전 텍스트 청소
            Console.WriteLine("플레이어 1: X 와 플레이어 2: O");
            Console.SetCursorPosition(0, 3);
            Console.WriteLine("플레이어 {0}의 차례", _turn.ToString());

            for (int i = 0; i < 17; i++)
            {
                Console.SetCursorPosition(i, 7);
                Console.Write("_");
                Console.SetCursorPosition(i, 10);
                Console.Write("_");
            }

            for (int i = 5; i < 14; i++)
            {
                Console.SetCursorPosition(5, i);
                Console.Write("|");
                Console.SetCursorPosition(11, i);
                Console.Write("|");
            }
            for(int i = 0; i < _tictactoe.Length; i++)
            {
                //틱택토의 패널위치번호값을 정확하게 계산하기 위해 좀 복잡한 값을 넣었다.
                //x의 값은 "_"기준 2칸 앞에 첫 숫자가 위치하고 6번째 "_"마다 다음 숫자가 기록된다.
                //다만, 3번째 숫자마다 첫 x값의 위치로 돌아와야 되기에 3의 나머지(%) 값으로 몇 번째 숫자의 위치인지 계산한다.
                //y의 값은 "|"기준 1칸 아래에 첫 숫자가 위치하고 3번째 "|"마다 다음 숫자가 기록된다.
                //다만, 플레이어 텍스트UI를 피해 5칸 아래로 추가로 내려왔으며(+5)
                //3번째 숫자가 기록될 때만 한 칸씩 내려와야 하기에 3의 몫(/) 값으로 위치를 계산한다.
                Console.SetCursorPosition(((i % 3) * 6 + 2), (((i / 3) * 3) + 6));
                Console.Write(_tictactoe[i]);
            }
            //모든 텍스트를 기록하고 마지막 위치(0, 15)에 위치해야 디버깅 텍스트가 틱택토 화면을 안가린다.
            Console.SetCursorPosition(0, 15);
        }
        static bool checkTictactoeWinner(string _playerMark, string[] _tictactoe)
        {
            for(int i = 0; i < 3; i++)//0,1,2 가로 윗줄을 훑으면서 세로방향 탐색
            {
                int j = i * 3;//0,3,6 세로 첫째행을 훑으면서 가로방향 탐색

                //i는 세로로 틱택토 검색. 세로로 모두 같은 모양이면 승리
                if (_tictactoe[i] == _playerMark)
                {
                    if (_tictactoe[i] == _tictactoe[i + 3] && _tictactoe[i] == _tictactoe[i + 6]) return true;
                }
                //j는 가로로 틱택토 검색. 가로로 모두 같은 모양이면 승리
                if (_tictactoe[j] == _playerMark)
                {
                    if (_tictactoe[j] == _tictactoe[j + 1] && _tictactoe[j] == _tictactoe[j + 2]) return true;
                }
            }
            //대각선
            if (_tictactoe[0] == _playerMark)
                if (_tictactoe[0] == _tictactoe[4] && _tictactoe[0] == _tictactoe[8]) return true;
            if (_tictactoe[2] == _playerMark)
                if (_tictactoe[2] == _tictactoe[4] && _tictactoe[2] == _tictactoe[6]) return true;

            return false;
        }
        static void Main(string[] args)
        {
            string[] tictactoe = new string[9];
            bool Turn = true;
            for (int i = 0; i < 9; i++)
                tictactoe[i] = (i + 1).ToString();

            Print(1, tictactoe);

            while(true)
            {
                int readNum;

                Console.Write("선택하실 패널 번호를 입력해주세요.: ");
                
                if(int.TryParse(Console.ReadLine(), out readNum))
                {
                    if(readNum <= 0 || readNum > 9)
                    {
                        Console.WriteLine("잘못된 입력입니다.");
                        continue;
                    }
                    tictactoe[readNum - 1] = Turn ? "X" : "O";
                    Print(Turn ? 2 : 1, tictactoe);
                    if(checkTictactoeWinner(Turn ? "X" : "O", tictactoe))
                    {
                        if (Turn) Console.WriteLine("플레이어 1의 승리!!");
                        else Console.WriteLine("플레이어 2의 승리!!");

                        break;
                    }
                    else
                    {
                        int MarkCheck = tictactoe.Count(item => item == "X" || item == "O");
                        if(MarkCheck == 9)
                        {
                            Console.WriteLine("이 게임은 무승부입니다.");
                            break;
                        }
                    }
                    Turn = !Turn;
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.");
                }
            }
        }
    }
}