namespace IgraNaMe4ove.GameObjects.Items
{
    using System;
    using System.Collections.Generic;

    public abstract class Item : GameObject
    {
        private const ConsoleColor ItemColour = ConsoleColor.Green;

        private int healthBonus;
        private int attackDamageBonus;
        private int manaBonus;
        private int rangeBonus;

        public Item(Position position, char symbol, int healthBonus, int attackDamageBonus, int manaBonus, int rangeBonus) : base(position, symbol, ItemColour)
        {
            this.HealthBonus = healthBonus;
            this.AtackDamageBonus = attackDamageBonus;
            this.ManaBonus = manaBonus;
            this.RangeBonus = rangeBonus;
        }

        public int HealthBonus
        {
            get
            {
                return this.healthBonus;
            }

            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("the health bonus cannot be negative !");
                }

                this.healthBonus = value;
            }
        }

        public int AtackDamageBonus
        {
            get
            {
                return this.attackDamageBonus;
            }

            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("the attackDamage bonus cannot be negative !");
                }

                this.attackDamageBonus = value;
            }
        }

        public int ManaBonus
        {
            get
            {
                return this.manaBonus;
            }

            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("the mana bonus cannot be negative !");
                }

                this.manaBonus = value;
            }
        }

        public int RangeBonus
        {
            get
            {
                return this.rangeBonus;
            }

            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("the range bonus cannot be negative !");
                }

                this.rangeBonus = value;
            }
        }

        public List<string> Status()
        {
            List<string> status = new List<string>();

            status.Add(string.Format("Item Type : [ {0} ]", this.GetType().Name));

            if (this.HealthBonus > 0)
            {
                status.Add(string.Format("Health Bonus : {0}", this.HealthBonus));
            }

            if (this.ManaBonus > 0)
            {
                status.Add(string.Format("Mana Bonus : {0}", this.ManaBonus));
            }

            if (this.AtackDamageBonus > 0)
            {
                status.Add(string.Format("Damage Bonus : {0}", this.AtackDamageBonus));
            }

            if (this.RangeBonus > 0)
            {
                status.Add(string.Format("Range Bonus  : {0}", this.RangeBonus));
            }

            return status;
        }
    }
}