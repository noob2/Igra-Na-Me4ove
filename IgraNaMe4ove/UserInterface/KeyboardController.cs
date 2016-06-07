namespace IgraNaMe4ove.UserInterface
{
    using System;
    using Interfaces;

    public class KeyboardController : IController
    {
        public event EventHandler Pause;

        public char ReadKey()
        {
            ConsoleKeyInfo keyPressed = Console.ReadKey();

            switch (keyPressed.Key)
            {
                case ConsoleKey.UpArrow:
                    return 'U';
                case ConsoleKey.DownArrow:
                    return 'D';
                case ConsoleKey.RightArrow:
                    return 'R';
                case ConsoleKey.LeftArrow:
                    return 'L';
                case ConsoleKey.S:
                    return 'S';
                case ConsoleKey.H:
                    return 'H';
                case ConsoleKey.I:
                    return 'I';
                case ConsoleKey.C:
                    return 'C';
                case ConsoleKey.A:
                    return 'A';
                case ConsoleKey.Y:
                    return 'Y';
                case ConsoleKey.K:
                    return 'K';
                case ConsoleKey.L:
                    return 'L';
                case ConsoleKey.G:
                    return 'G';
                default:
                    return '\0';
            }
        }
    }
}
