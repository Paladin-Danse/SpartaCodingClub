using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame
{
    internal class Snake
    {
        bool isDead;
        Point headPos;
        public Point getHeadPosition
        {
            get { return headPos; }
        }
        int bodySize;
        Direction eDirection;
        public Direction direction
        {
            get { return eDirection; }
            set
            {
                eDirection = value;
            }
        }
        char bodySym = '■';
        char headSym
        {
            get
            {
                switch (eDirection)
                {
                    case Direction.LEFT:
                        return '◀';
                    case Direction.RIGHT:
                        return '▶';
                    case Direction.UP:
                        return '▲';
                    case Direction.DOWN:
                        return '▼';
                    default:
                        return '?';
                }
            }
        }
        
        LinkedList<Point> snakeBody;

        public int snakeBodyCnt { get { return snakeBody.Count; } }

        public Snake(Point _point, int _size, Direction _direction)
        {
            headPos = new Point(_point.x, _point.y, headSym);
            bodySize = _size;
            snakeBody = new LinkedList<Point>();
            for(int i = 0; i < bodySize; i++)
            {
                snakeBody.AddLast(new Point(headPos.x, headPos.y, bodySym));
            }
            eDirection = _direction;
        }

        public void Move()
        {
            if(snakeBody.First != null) NextBodyMove(snakeBody.First);

            switch (eDirection)
            {
                case Direction.LEFT:
                    headPos.x += -2;
                    break;
                case Direction.RIGHT:
                    headPos.x += 2;
                    break;
                case Direction.UP:
                    headPos.y += -1;
                    break;
                case Direction.DOWN:
                    headPos.y += 1;
                    break;
                default:
                    break;
            }
        }

        public bool bodyHitCheck()
        {
            bool isBodyHit = false;

            foreach (Point body in snakeBody)
            {
                if (headPos.IsHit(body))
                {
                    isBodyHit = true;
                    break;
                }
            }
            return isBodyHit;
        }
        public void NextBodyMove(LinkedListNode<Point> _point)
        {
            if (_point != null)
            {
                NextBodyMove(_point.Next);
                if (_point.Previous == null)
                {
                    _point.Value.x = headPos.x;
                    _point.Value.y = headPos.y;
                    return;
                }
                _point.Value.x = _point.Previous.Value.x;
                _point.Value.y = _point.Previous.Value.y;
            }
            return;
        }

        public void getNewBody()
        {
            snakeBody.AddLast(new Point(snakeBody.Last.Value.x, snakeBody.Last.Value.y, bodySym));
        }
        public void Draw()
        {
            Console.SetCursorPosition(headPos.x, headPos.y);
            Console.Write(headSym);
            foreach(Point body in snakeBody)
            {
                Console.SetCursorPosition(body.x, body.y);
                Console.Write(body.sym);
            }
        }
        public void Clear()
        {
            headPos.Clear();
            foreach(Point body in snakeBody)
            {
                body.Clear();
            }
        }
    }
}
