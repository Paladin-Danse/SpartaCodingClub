using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    internal class Player
    {
        //레벨
        int level;
        string name;
        //직업
        CLASS characterClass;
        //공격력
        int defaultAttackPoint;
        public int buffAttackPoint;
        public int currentAttackPoint { get { return defaultAttackPoint + buffAttackPoint; } }
        //방어력
        int defaultDefencePoint;
        public int buffDefencePoint;
        public int currentDefencePoint { get { return defaultDefencePoint + buffDefencePoint; } }
        //체력
        int defaultMaxHealth;
        public int buffMaxHealth;
        public int currentMaxHealth { get { return defaultMaxHealth + buffMaxHealth; } }
        int currentHealth;
        public int getHP { get { return currentHealth; } }

        public Player(int _level, string _name, CLASS _class, int _attackPoint, int _defencePoint, int _health)
        {
            level = _level;
            name = _name;
            characterClass = _class;

            defaultAttackPoint = _attackPoint;
            buffAttackPoint = 0;

            defaultDefencePoint = _defencePoint;
            buffDefencePoint = 0;

            defaultMaxHealth = _health;
            buffMaxHealth = 0;

            currentHealth = currentMaxHealth;
        }

        public void PlayerInfo()
        {
            Console.WriteLine($"이름 : {name}");
            Console.WriteLine($"Lv. {level:D2}");
            switch (characterClass)
            {
                case CLASS.CLASS_WARRIOR:
                    Console.WriteLine("직업 : 전사");
                    break;
                case CLASS.CLASS_RANGER:
                    Console.WriteLine("직업 : 레인저");
                    break;
                case CLASS.CLASS_WIZARD:
                    Console.WriteLine("직업 : 마법사");
                    break;
                case CLASS.CLASS_CRUSADER:
                    Console.WriteLine("직업 : 성전사");
                    break;
                default:
                    Console.WriteLine("직업 : 없음");
                    break;
            }
            //버프된 능력치는 0이 아닐 때 뒤에 (+n) 형식으로 표기됨. 0이라면 표시되지 않음.
            Console.WriteLine($"공격력 : {currentAttackPoint} {(buffAttackPoint != 0 ? $"({(buffAttackPoint >= 0 ? "+" : "")}"+buffAttackPoint+")" : "")}");
            Console.WriteLine($"방어력 : {currentDefencePoint} {(buffDefencePoint != 0 ? $"({(buffDefencePoint >= 0 ? "+" : "")}" + buffDefencePoint + ")" : "")}");
            Console.WriteLine($"체 력 : {currentHealth}/{currentMaxHealth} {(buffMaxHealth != 0 ? $"({(buffMaxHealth >= 0 ? "+" : "")}" + buffMaxHealth + ")" : "")}");
            Console.WriteLine($"Gold : {Inventory.Instance.inventoryGold} G\n");
        }
        public void Damaged(int _damage)
        {
            currentHealth -= _damage;
            if (currentHealth < 0) currentHealth = 0;
        }
        public void Healed(int _heal)
        {
            currentHealth += _heal;
            if (currentHealth > currentMaxHealth) currentHealth = currentMaxHealth;
        }
    }
}
