namespace IgraNaMe4ove
{
    using System;
    using Engine;
    using Engine.Factories;
    using GameObjects.Characters;
    using Interfaces;
    using UserInterface;

    public class Program
    {
        public static void Main()
        {
            Character player = new Character(new Position(0, 0), 'C', ConsoleColor.Yellow, 0, 0, 0, 0, CharacterRace.Ashe);
            IRenderer renderer = new ConsoleRenderer();
            IInputHandler inputHandler = new ConsoleInputHandler();
            IController controller = new KeyboardController();
            CommandFactory commandFactory = new CommandFactory();
            GameEngine gameEngine = new GameEngine(player, renderer, inputHandler, controller, commandFactory);

            Console.CursorVisible = false;

            gameEngine.Run();
        }
    }
}
