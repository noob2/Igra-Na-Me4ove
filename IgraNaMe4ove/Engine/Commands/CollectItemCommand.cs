namespace IgraNaMe4ove.Engine.Commands
{
    using System.Collections.Generic;
    using System.Linq;
    using Attributes;
    using Exceptions;
    using GameObjects.Items;
    
    [Command]
    public class CollectItemCommand : Command
    {
        public CollectItemCommand(GameEngine gameEngine)
            : base(gameEngine)
        {
        }

        public override void Execute(string commandName)
        {
            Item thisItem = (Item)GameEngine.ObjectsOnMap
                .FirstOrDefault(item => item.Position.X == GameEngine.Champion.Position.X &&
                item.Position.Y == GameEngine.Champion.Position.Y &&
                item is Item);

            if (thisItem == null)
            {
                throw new ItemNotFoundException("there is no item to pick there");
            }

            List<string> itemStatus = thisItem.Status();

            // draws the item bonuses
            int offsetFromLeft = Constants.MapWidth + 1;
            for (int i = 0; i < itemStatus.Count; i++)
            {
                int offsetFromTop = i + 6;
                GameEngine.Renderer.WriteAt(new Position(offsetFromLeft, offsetFromTop), itemStatus[i]);
            }

            // asks the player to pick the item
            string question = "pick item ? (Y/N)";
            Position questionPosition = new Position(Constants.MapWidth + 5, 13);
            GameEngine.Renderer.WriteAt(questionPosition, question);

            char keyPressed = GameEngine.Controller.ReadKey();

            if (keyPressed == 'Y')
            {
                GameEngine.Champion.AddItemToInventory(thisItem);
                GameEngine.ObjectsOnMap.Remove(thisItem);
            }

            // clear the item status and question from the screen
            for (int i = 0; i < itemStatus.Count; i++)
            {
                string spacesToWrite = new string(' ', itemStatus[i].Length);
                int offsetFromTop = i + 6;
                GameEngine.Renderer.WriteAt(new Position(offsetFromLeft, offsetFromTop), spacesToWrite);
            }

            string spacesForQuestion = new string(' ', question.Length);
            GameEngine.Renderer.WriteAt(questionPosition, spacesForQuestion);
        }
    }
}