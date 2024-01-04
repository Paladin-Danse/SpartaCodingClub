namespace _8_2
{
    internal class Program
    {
        class Person
        {
            private string name;
            private int age;
            public Person()
            {
                name = "UnKnown";
                age = 0;
                Console.WriteLine("Person 객체 생성");
            }
            public Person(string newName, int newAge)
            {
                name = newName;
                age = newAge;
                Console.WriteLine("Person 객체 생성");
            }
            public void PrintInfo()
            {
                Console.WriteLine($"Name: {name}, Age: {age}");
            }

            ~Person()
            {

            }
        }



        
        static void Main(string[] args)
        {
            Person person1 = new Person();
            Person person2 = new Person("John", 25);
            person2.PrintInfo();
        }
    }
}
