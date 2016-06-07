namespace IgraNaMe4ove.GameObjects.Enemies
{
    using Attributes;

    [Enemy]
    public class Ork : Enemy
    {
        private const char OrkSymbol = 'O';
        private const int OrkHealth = 50;
        private const int OrkRange = 1;
        private const int OrkDamage = 20;

        public Ork(Position position) : base(position, OrkSymbol, OrkHealth, OrkRange, OrkDamage)
        {
        }
    }
}
