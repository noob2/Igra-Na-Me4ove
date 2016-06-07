namespace IgraNaMe4ove.GameObjects
{
    using System;
    using Engine;
    using Exceptions;

    public class GameObject
    {
        private Position position;
        private char symbol;

        public GameObject(Position position, char symbol, ConsoleColor colour)
        {
            this.Colour = colour;
            this.Position = position;
            this.Symbol = symbol;
        }

        public Position Position
        {
            get
            {
                return this.position;
            }

            set
            {
                if (value.X < 0
                    || value.Y < 0
                    || value.X >= Constants.MapWidth
                    || value.Y >= Constants.MapHeight)
                {
                    throw new ObjectOutOfMapException("Specified coordinates are outside map.");
                }

                this.position = value;
            }
        }

        public char Symbol
        {
            get
            {
                return this.symbol;
            }

            protected set
            {
                this.symbol = value;
            }
        }

        public ConsoleColor Colour { get; protected set; }
    }
}
