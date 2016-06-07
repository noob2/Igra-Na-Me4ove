namespace IgraNaMe4ove.GameObjects.Enemies
{
    using Attributes;

    [Enemy]
    public class Wolf : Enemy
    {
        private const char WolfSymbol = 'W';
        private const int WolfHealth = 50;
        private const int WolfRange = 1;
        private const int WolfDamage = 20;

        public Wolf(Position position) : base(position, WolfSymbol, WolfHealth, WolfRange, WolfDamage)
        {
        }
    }
}
