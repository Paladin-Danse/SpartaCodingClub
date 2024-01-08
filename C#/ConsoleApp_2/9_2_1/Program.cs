﻿namespace _9_2_1
{
    public class Unit
    {
        public virtual void Move()
        {
            Console.WriteLine("두발로 걷기");
        }

        public void Attack()
        {
            Console.WriteLine("Unit 공격");
        }
    }

    public class Marine : Unit
    {

    }

    public class Zergling : Unit
    {
        public override void Move()
        {
            Console.WriteLine("네발로 걷기");
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            //Marine marine = new Marine();
            //marine.Move();
            //marine.Attack();

            //Zergling zergling = new Zergling();
            //zergling.Move();
            //zergling.Attack();

            //Unit
            List<Unit> list = new List<Unit>();
            list.Add(new Marine());
            list.Add(new Zergling());

            foreach(Unit unit in list)
            {
                unit.Move();
            }
        }
    }
}