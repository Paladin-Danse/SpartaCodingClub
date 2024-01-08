namespace _10_1
{
    internal class Program
    {
        //제너릭
        class Stack<T>
        {
            private T[] elements;
            private int top;

            public Stack()
            {
                elements = new T[100];
                top = 0;
            }

            public void Push(T item)
            {
                elements[top++] = item;
            }
            public T Pop()
            {
                return elements[--top];
            }
        }
        static void Main(string[] args)
        {
            
        }
    }
}
