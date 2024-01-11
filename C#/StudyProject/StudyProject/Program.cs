namespace StudyProject
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Queue myQueue = new Queue(5);

            myQueue.Enqueue("1");
            myQueue.Enqueue("2");
            myQueue.Enqueue("3");    
            myQueue.Enqueue("4");
            myQueue.Enqueue("5");

            myQueue.PrintQueue();

            var str = myQueue.Dequeue();
            Console.WriteLine("Dequeue : " + str);

            myQueue.PrintQueue();

        }
        public class Queue
        {
            private int front;
            private int rear;
            private int maxSize;
            private string[] arr;

            public Queue(int maxSize)
            {
                front = 0;
                rear = -1;
                this.maxSize = maxSize;
                arr = new string[maxSize];
            }

            public bool IsFull()
            {
                return rear >= maxSize - 1;
            }
            public bool IsEmpty()
            {
                return rear < front;
            }
            public void Enqueue(string data)
            {
                if(IsFull())
                {
                    Console.Error.WriteLine("Queue Full");
                    return;
                }
                arr[++rear] = data;
            }

            public string Dequeue()
            {
                if(IsEmpty())
                {
                    Console.Error.WriteLine("Queue Empty");
                    return null;
                }
                return arr[front++];
            }
            public void PrintQueue()
            {
                for(int i = 0; i < rear; i++)
                {
                    Console.Write(arr[i] + " ");
                }
                Console.WriteLine();
            }
        }
    }
}
