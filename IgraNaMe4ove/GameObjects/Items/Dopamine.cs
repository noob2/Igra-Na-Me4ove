namespace IgraNaMe4ove.GameObjects.Items
{
    using Attributes;

    [Item]
    public class Dopamine : Item
    {
        private const char DopamineSymbol = 'D';
        private const int DopamineHealthBonus = 0;
        private const int DopamineAttackDamageBonus = 10;
        private const int DopamineManaBonus = 0;
        private const int DopamineRangeBonus = 1;

        public Dopamine(Position position) : base(position, DopamineSymbol, DopamineHealthBonus, DopamineAttackDamageBonus, DopamineManaBonus, DopamineRangeBonus)
        {
        }
    }
}
