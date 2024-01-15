namespace _11_1
{
    internal class Program
    {

        public interface IMovable
        {
            void Move(int x, int y);
        }

        public class Player : IMovable
        {
            public void Move(int x, int y)
            {
                
            }
        }
        public class Enemy : IMovable
        {
            public void Move(int x, int y)
            {

            }
        }
        static void Main(string[] args)
        {
            IMovable movableObject1 = new Player();
            IMovable movableObject2 = new Enemy();

        }
    }
}
