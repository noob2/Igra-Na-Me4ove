namespace IgraNaMe4ove.UserInterface
{
    using System;
    using Engine;
    using Interfaces;

    public class ConsoleRenderer : IRenderer
    {
        public void Clear()
        {
            Console.Clear();
        }

        public void Colour(ConsoleColor colour)
        {
            Console.ForegroundColor = colour;
        }

        public void WriteAt(Position position, char messege, ConsoleColor colour)
        {
            Console.ForegroundColor = colour;
            Console.SetCursorPosition(position.X, position.Y);
            Console.Write(messege);
            Console.SetCursorPosition(Constants.MapWidth + 1, Constants.MapHeight + 1);
            Console.ResetColor();
        }

        public void WriteAt(Position position, string messege)
        {
            Console.SetCursorPosition(position.X, position.Y);
            Console.Write(messege);
            Console.SetCursorPosition(Constants.MapWidth + 1, Constants.MapHeight + 1);
        }
    }
}
