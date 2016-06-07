namespace IgraNaMe4ove.Engine.Commands
{
    using Attributes;
    using Exceptions;

    [Command]
    public class MoveDownCommand : Command
    {
        public MoveDownCommand(GameEngine gameEngine)
            : base(gameEngine)
        {
        }

        public override void Execute(string commandName)
        {
            Position positionAfterMovingDown = new Position(GameEngine.Champion.Position.X, GameEngine.Champion.Position.Y + 1);

            bool isFreePosition = GameEngine.IsFreePosition(positionAfterMovingDown);

            if (isFreePosition)
            {
                GameEngine.Champion.Position = positionAfterMovingDown;
            }
            else
            {
                throw new CollisionException("u can't move over wall or champion");
            }
        }
    }
}
