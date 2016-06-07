namespace IgraNaMe4ove.GameObjects
{
    using System;

    public class Wall : GameObject
    {
        private const char WallSymbol = '@';
        private const ConsoleColor WallColour = ConsoleColor.Gray;

        public Wall(Position position) : base(position, WallSymbol, WallColour)
        {
        }
    }
}
