namespace IgraNaMe4ove.Engine.Commands
{
    using Attributes;

    [Command]
    public class OpenInventoryCommand : Command
    {
        public OpenInventoryCommand(GameEngine gameEngine)
            : base(gameEngine)
        {
        }

        public override void Execute(string commandName)
        {
            GameEngine.Renderer.Clear();

            if (GameEngine.Champion.Inventory.Count == 0)
            {
                GameEngine.Renderer.WriteAt(new Position(0, 1), "Inventory is empty\npress any key to exit the Inventory");
                GameEngine.Controller.ReadKey();
                GameEngine.DrawMap();
                return;
            }

            for (int i = 0; i < GameEngine.Champion.Inventory.Count; i++)
            {
                GameEngine.Renderer.WriteAt(new Position(0, i), i + 1 + ". " + GameEngine.Champion.Inventory[i].GetType().Name);
            }

            GameEngine.Renderer.WriteAt(new Position(0, GameEngine.Champion.Inventory.Count + 1), "enter the number, corresponding to the item,\nto use it or any other key to exit the Inventory");

            int choice = int.Parse(GameEngine.InputHandler.ReadLine()) - 1;

            GameEngine.Champion.UseItem(choice);

            GameEngine.DrawMap();
        }
    }
}
