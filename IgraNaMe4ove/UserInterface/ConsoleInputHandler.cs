namespace IgraNaMe4ove.UserInterface
{
    using System;
    using Interfaces;

    public class ConsoleInputHandler : IInputHandler
    {
        public string ReadLine()
        {
            return Console.ReadLine();
        }
    }
}
