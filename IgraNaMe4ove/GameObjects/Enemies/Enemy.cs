namespace IgraNaMe4ove.GameObjects.Enemies
{
    using System;
    using System.Collections.Generic;
    using Characters;
    using Interfaces;

    public abstract class Enemy : GameObject, IChampionAttackable, IDestroyable
    {
        private const ConsoleColor EnemyColour = ConsoleColor.Red;
        private int health;
        private int range;
        private int attackDamage;
        
        public Enemy(Position position, char symbol, int health, int range, int attackDamage) : base(position, symbol, EnemyColour)
        {
            this.Health = health;
            this.Range = range;
            this.AttackDamage = attackDamage;
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

            protected set
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

            protected set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("attackDamage", "attack damage must be positive !");
                }

                this.attackDamage = value;
            }
        }

        public List<string> Status()
        {
            List<string> status = new List<string>();

            status.Add(string.Format("Enemy Type : [ {0} ]", this.GetType().Name));
            status.Add(string.Format("Health : {0}", this.Health));
            status.Add(string.Format("Range  : {0}", this.Range));
            status.Add(string.Format("Damage : {0}", this.AttackDamage));

            return status;
        }

        public void Attack(Character champion)
        {
            if (champion.Health - this.AttackDamage < 0)
            {
                champion.Health = 0;
            }
            else
            {
                champion.Health -= this.AttackDamage;
            }
        }
    }
}
