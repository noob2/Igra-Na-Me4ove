namespace IgraNaMe4ove.GameObjects.Items
{
    using Attributes;

    [Item]
    public class HealthPotion : Item
    {
        private const char HealthPotionSymbol = '+';
        private const int HealthPotionHealthBonus = 20;
        private const int HealthPotionAttackDamageBonus = 0;
        private const int HealthPotionManaBonus = 0;
        private const int HealthPotionRangeBonus = 0;

        public HealthPotion(Position position)
            : base(position, HealthPotionSymbol, HealthPotionHealthBonus, HealthPotionAttackDamageBonus, HealthPotionManaBonus, HealthPotionRangeBonus)
        {
        }
    }
}
