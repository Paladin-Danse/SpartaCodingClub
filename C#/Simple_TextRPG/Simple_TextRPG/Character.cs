using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_TextRPG
{
    internal interface ICharacter
    {
        string Name { get; }
        int Health { get; }
        int Attack { get; }
        bool IsDead => Health <= 0;
        void TakeDamage(int _damage);
    }
    internal class Warrior : ICharacter
    {
        public string Name { get; private set; }
        public int Health { get; private set; }
        public int Attack { get; private set; }
        public void TakeDamage(int _damage)
        {

        }
    }

    internal class Monster : ICharacter
    {
        public string Name { get; private set; }
        public int Health { get; private set; }
        public int Attack { get; private set; }
        public void TakeDamage(int _damage)
        {

        }
    }
    internal class Goblin : Monster
    {

    }
    internal class Dragon : Monster
    {

    }
}
