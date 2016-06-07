namespace IgraNaMe4ove.GameObjects.Items
{
    using Attributes;

    [Item]
    public class ManaPotion : Item
    {
        private const char ManaPotionSymbol = 'M';
        private const int ManaPotionHealthBonus = 0;
        private const int ManaPotionAttackDamageBonus = 0;
        private const int ManaPotionManaBonus = 20;
        private const int ManaPotionRangeBonus = 0;

        public ManaPotion(Position position)
            : base(position, ManaPotionSymbol, ManaPotionHealthBonus, ManaPotionAttackDamageBonus, ManaPotionManaBonus, ManaPotionRangeBonus)
        {
        }
    }
}
