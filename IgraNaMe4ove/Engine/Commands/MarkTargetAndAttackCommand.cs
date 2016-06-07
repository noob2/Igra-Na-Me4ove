namespace IgraNaMe4ove.Engine.Commands
{
    using System;
    using System.Linq;
    using Attributes;
    using GameObjects;
    using GameObjects.Enemies;
    
    [Command]
    public class MarkTargetAndAttackCommand : Command
    {
        public MarkTargetAndAttackCommand(GameEngine gameEngine)
            : base(gameEngine)
        {
        }

        public override void Execute(string commandName)
        {
            Position enemyPosition = new Position();

            foreach (GameObject obj in GameEngine.ObjectsOnMap)
            {
                if (obj is Enemy && GameEngine.IsEnemyInRange(obj as Enemy))
                {
                    Enemy enemy = obj as Enemy;

                    var symbolToReplaceWithHashtag = GameEngine.ObjectsOnMap.FirstOrDefault(e => e.Position.X == enemyPosition.X && e.Position.Y == enemyPosition.Y);

                    if (symbolToReplaceWithHashtag != null)
                    {
                        GameEngine.Renderer.WriteAt(new Position(enemyPosition.X, enemyPosition.Y), symbolToReplaceWithHashtag.Symbol, enemy.Colour);
                    }

                    enemyPosition = new Position(enemy.Position.X, enemy.Position.Y);
                    GameEngine.OldMapArray[enemyPosition.X, enemyPosition.Y] = '#';
                    GameEngine.Renderer.WriteAt(enemyPosition, '#', ConsoleColor.Blue);

                    GameEngine.RedrawEnemyStatusOnMap(enemy);

                    char keyPressed = GameEngine.Controller.ReadKey();

                    if (keyPressed == 'A')
                    {
                        GameEngine.Champion.Attack(enemy);

                        if (enemy.Health == 0)
                        {
                            GameEngine.ObjectsOnMap.Remove(enemy);
                        }

                        break;
                    }
                    else if (keyPressed != 'S')
                    {
                        break;
                    }
                }
            }
        }
    }
}
