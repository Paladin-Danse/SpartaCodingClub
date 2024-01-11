using System.Drawing;

namespace SnakeGame
{
    public enum Direction
    {
        LEFT = 0,
        RIGHT,
        UP,
        DOWN
    }
    internal class Program
    {
        static int MapX = 80;
        static int MapY = 20;
        static int ScorePosX = 30;
        static int ScorePosY = 25;
        static int creatTime = 0;
        static bool isGameOver = false;
        static void Main(string[] args)
        {
            //아래에 메뉴를 표시하기 위해 10칸 정도의 y값을 확보
            //벽을 그리기엔 모자라서 늘림
            Console.SetWindowSize(MapX + 2, MapY + 10);
            List<Point> Wall = new List<Point>();
            int eatFoodCnt = 0;
            for(int y = 0; y <= MapY; y++)
            {
                Wall.Add(new Point(0, y, '▩'));
                for(int x = 2; x < MapX; x+=2)
                {
                    if (y == 0 || y == MapY)
                        Wall.Add(new Point(x, y, '▩'));
                }
                Wall.Add(new Point(MapX, y, '▩'));
            }

            foreach (Point wallPoint in Wall)
            {
                Console.SetCursorPosition(wallPoint.x, wallPoint.y);
                Console.Write(wallPoint.sym);
            }

            // 뱀의 초기 위치와 방향을 설정하고, 그립니다.
            Point p = new Point(4, 5, '*');
            Snake snake = new Snake(p, 4, Direction.RIGHT);
            snake.Draw();

            // 음식의 위치를 무작위로 생성하고, 그립니다.
            FoodCreator foodCreator = new FoodCreator(MapX, MapY, '$');
            List<Point> foodList = new List<Point>();
            Point food = foodCreator.CreateFood();
            foodList.Add(food);
            food.Draw();

            // 게임 루프: 이 루프는 게임이 끝날 때까지 계속 실행됩니다.
            while (!isGameOver)
            {
                snake.Clear();
                foodList.ForEach(food => food.Clear());
                creatTime++;//음식 생성 쿨타임
                //키가 입력받았을 때의 처리
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo inputKey = Console.ReadKey(true);
                    // 키 입력이 있는 경우에만 방향을 변경합니다.
                    switch (inputKey.Key)
                    {
                        case ConsoleKey.LeftArrow:
                            if(snake.direction != Direction.RIGHT) snake.direction = Direction.LEFT;
                            break;
                        case ConsoleKey.RightArrow:
                            if (snake.direction != Direction.LEFT) snake.direction = Direction.RIGHT;
                            break;
                        case ConsoleKey.UpArrow:
                            if (snake.direction != Direction.DOWN) snake.direction = Direction.UP;
                            break;
                        case ConsoleKey.DownArrow:
                            if (snake.direction != Direction.UP) snake.direction = Direction.DOWN;
                            break;
                        default:
                            break;
                    }
                }
                // 이동, 음식 먹기, 충돌 처리 등의 로직을 완성하세요.
                snake.Move();//이동
                if(creatTime > 15)//15번의 반복이 돌아 왔을 때
                {
                    Point newFoodPoint;
                    do
                    {
                        newFoodPoint = foodCreator.CreateFood();
                    } while (newFoodPoint.IsHit(snake.getHeadPosition));
                    foodList.Add(foodCreator.CreateFood());
                    creatTime = 0;
                }
                //음식이 게임화면에 있을 때만
                if (foodList.Count > 0)
                {
                    foreach (Point foodPoint in foodList)
                    {
                        if (snake.getHeadPosition.IsHit(foodPoint))//음식과 부딪혔는지?
                        {
                            eatFoodCnt++;
                            snake.getNewBody();
                            foodList.Remove(foodPoint);
                            break;
                        }
                    }
                    foodList.ForEach(food => food.Draw());
                }
                //뱀 머리가 몸통이랑 부딪혔을 때
                if (snake.bodyHitCheck()) isGameOver = true;
                //뱀 머리가 벽에 부딪혔을 때
                foreach(Point wallPoint in Wall)
                {
                    if(snake.getHeadPosition.IsHit(wallPoint))
                    {
                        isGameOver = true;
                        break;
                    }
                }
                
                snake.Draw();

                // 뱀의 상태를 출력합니다 (예: 현재 길이, 먹은 음식의 수 등)
                Console.SetCursorPosition(ScorePosX, ScorePosY);
                Console.Write($"뱀의 길이 : {snake.snakeBodyCnt}");
                Console.SetCursorPosition(ScorePosX, ScorePosY + 1);
                Console.Write($"뱀이 먹은 $ : {eatFoodCnt}");

                if(!isGameOver) Thread.Sleep(200); // 게임 속도 조절 (이 값을 변경하면 게임의 속도가 바뀝니다)

                
            }
            Console.SetCursorPosition((MapX / 2) - 8, MapY / 2);
            Console.WriteLine("게 임 오 버");
        }
    }
    public class Point
    {
        public int x { get; set; }
        public int y { get; set; }
        public char sym { get; set; }

        // Point 클래스 생성자
        public Point(int _x, int _y, char _sym)
        {
            x = _x;
            y = _y;
            sym = _sym;
        }

        // 점을 그리는 메서드
        public void Draw()
        {
            Console.SetCursorPosition(x, y);
            Console.Write(sym);
        }
        public void Draw(string _sym)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(_sym);
        }

        // 점을 지우는 메서드
        public void Clear()
        {
            //특수문자가 크기를 2칸씩 먹어서 공백으로 지울 때도 2칸씩 써야됨에 따라 string변수로 씀.
            Draw("  ");
        }

        // 두 점이 같은지 비교하는 메서드
        public bool IsHit(Point p)
        {
            return p.x == x && p.y == y;
        }
    }
}
