using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame
{
    internal class FoodCreator
    {
        int mapSizeX;
        int mapSizeY;
        char foodSym;
        public FoodCreator(int _mapSizeX, int _mapSizeY, char _sym)
        {
            mapSizeX = _mapSizeX;
            mapSizeY = _mapSizeY;
            foodSym = _sym;
        }
        public Point CreateFood()
        {
            //X값은 특수문자로 인해 2칸씩 잡아먹기 때문에 반드시 홀수나 짝수값만 나와야 같은 위치처럼 보이는 다른 위치값이 나오질 않는다.
            //아랫값은 짝수로 1~39까지 랜덤 값이 나오면 나온 값에서 2배로 곱해 2~78사이의 짝수값이 나온다.
            int randomX = new Random().Next(1, mapSizeX / 2) * 2;
            int randomY = new Random().Next(1, mapSizeY);
            
            return new Point(randomX, randomY, foodSym);
        }
    }
}
