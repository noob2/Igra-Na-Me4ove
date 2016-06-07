namespace IgraNaMe4ove.GameObjects.Characters
{
    using System;
    using System.Collections.Generic;
    using Enemies;
    using Interfaces;
    using Items;

    public class Character : GameObject, ICollect
    {
        private readonly List<Item> inventory;
        private int mana;
        private int health;
        private int range;
        private int attackDamage;

        public Character(Position position, char symbol, ConsoleColor colour, int health, int mana, int range, int attackDamage, CharacterRace race) : base(position, symbol, colour)
        {
            this.Health = health;
            this.Mana = mana;
            this.Range = range;
            this.AttackDamage = attackDamage;
            this.Race = race;

            this.inventory = new List<Item>();
        }

        public CharacterRace Race { get; set; }

        public int Mana
        {
            get
            {
                return this.mana;
            }

            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("mana", "mana must be positive !");
                }

                this.mana = value;
            }
        }

        public int Health
        {
            get
            {
                return this.health;
            }

            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("health", "health must be positive !");
                }

                this.health = value;
            }
        }

        public int Range
        {
            get
            {
                return this.range;
            }

            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("range", "range must be positive !");
                }

                this.range = value;
            }
        }

        public int AttackDamage
        {
            get
            {
                return this.attackDamage;
            }

            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("attackDamage", "attack damage must be positive !");
                }

                this.attackDamage = value;
            }
        }

        public List<Item> Inventory
        {
            get
            {
                return this.inventory;
            }
        }
        
        public void UseItem(int positionInInventory)
        {
            if (positionInInventory >= this.inventory.Count)
            {
                Console.WriteLine("\n Invalid choice");
                return;
            }

            this.Mana += this.inventory[positionInInventory].ManaBonus;
            this.Health += this.inventory[positionInInventory].HealthBonus;
            this.AttackDamage += this.inventory[positionInInventory].AtackDamageBonus;
            this.Range += this.inventory[positionInInventory].RangeBonus;

            this.inventory.RemoveAt(positionInInventory);
        }

        public void Attack(Enemy target)
        {
            if (target.Health < this.AttackDamage)
            {
                target.Health = 0;
            }
            else
            {
                target.Health -= this.AttackDamage;
            }
        }

        public void AddItemToInventory(Item item)
        {
            this.inventory.Add(item);
        }

        public List<string> Status()
        {
            List<string> status = new List<string>();

            status.Add(string.Format("[ {0} ]", this.Race.ToString()));

            status.Add(string.Format("Health : {0}", this.Health));

            if (this.Mana > 0)
            {
                status.Add(string.Format("Mana   : {0}", this.Mana));
            }

            status.Add(string.Format("Range  : {0}", this.Range));
            status.Add(string.Format("Damage : {0}", this.AttackDamage));

            return status;
        }

        public void SetPlayerStats()
        {
            switch (this.Race)
            {
                case CharacterRace.Ashe:
                    this.Symbol = 'A';
                    this.Health = 100;
                    this.Mana = this.mana;
                    this.Range = 4;
                    this.AttackDamage = 30;
                    break;
                case CharacterRace.Kassadin:
                    this.Symbol = 'K';
                    this.Health = 120;
                    this.Mana = this.mana;
                    this.Range = 1;
                    this.AttackDamage = 45;
                    break;
                case CharacterRace.Garen:
                    this.Symbol = 'G';
                    this.Health = 200;
                    this.Mana = this.mana;
                    this.Range = 4;
                    this.AttackDamage = 25;
                    break;
                case CharacterRace.Lissandra:
                    this.Symbol = 'L';
                    this.Health = 100;
                    this.Mana = this.mana;
                    this.Range = 4;
                    this.AttackDamage = 32;
                    break;
                default:
                    throw new ArgumentException("Unknown player race.");
            }
        }
    }
}
