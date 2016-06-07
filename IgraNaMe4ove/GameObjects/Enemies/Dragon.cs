namespace IgraNaMe4ove.GameObjects.Enemies
{
    using Attributes;

    [Enemy]
    public class Dragon : Enemy
    {
        private const char DragonSymbol = 'D';
        private const int DragonHealth = 50;
        private const int DragonRange = 1;
        private const int DragonDamage = 20;

        public Dragon(Position position) : base(position, DragonSymbol, DragonHealth, DragonRange, DragonDamage)
        {
        }
    }
}
