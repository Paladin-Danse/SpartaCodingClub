using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Simple_TextRPG
{
    internal interface IItem
    {
        string Name { get; }
        public void Use(Warrior warrior);
    }
    internal class HealthPotion : IItem
    {
        public string Name { get; private set; }
        public void Use(Warrior warrior)
        {

        }
    }
    internal class StrengthPotion : IItem
    {
        public string Name { get; private set;}
        public void Use(Warrior warrior)
        {

        }
    }
}
