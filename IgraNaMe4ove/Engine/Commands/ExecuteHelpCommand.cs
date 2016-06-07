namespace IgraNaMe4ove.Engine.Commands
{
    using System.IO;
    using Attributes;
    
    [Command]
    public class ExecuteHelpCommand : Command
    {
        public ExecuteHelpCommand(GameEngine gameEngine)
            : base(gameEngine)
        {
        }

        public override void Execute(string commandName)
        {
            GameEngine.Renderer.Clear();
            string helpInfo = File.ReadAllText("../../helpInfo.txt");
            GameEngine.Renderer.WriteAt(new Position(0, 0), helpInfo);
            GameEngine.Controller.ReadKey();
            GameEngine.DrawMap();
        }
    }
}
