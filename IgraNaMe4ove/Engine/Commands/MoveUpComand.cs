namespace IgraNaMe4ove.Engine.Commands
{
    using Attributes;
    using Exceptions;

    [Command]
    public class MoveUpCommand : Command
    {
        public MoveUpCommand(GameEngine gameEngine)
            : base(gameEngine)
        {
        }

        public override void Execute(string commandName)
        {
            Position positionAfterMovingUp = new Position(GameEngine.Champion.Position.X, GameEngine.Champion.Position.Y - 1);

            bool isFreePosition = GameEngine.IsFreePosition(positionAfterMovingUp);

            if (isFreePosition)
            {
                GameEngine.Champion.Position = positionAfterMovingUp;
            }
            else
            {
                throw new CollisionException("u can't move over wall or champion");
            }
        }
    }
}
