namespace IgraNaMe4ove.Engine.Commands
{
    public abstract class Command
    {
        protected Command(GameEngine gameEngine)
        {
            this.GameEngine = gameEngine;
        }

        public GameEngine GameEngine { get; set; }

        public abstract void Execute(string commandName);
    }
}
