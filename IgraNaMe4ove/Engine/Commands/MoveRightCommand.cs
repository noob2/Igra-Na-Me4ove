namespace IgraNaMe4ove.Engine.Commands
{
    using Attributes;
    using Exceptions;

    [Command]
    public class MoveRightCommand : Command
    {
        public MoveRightCommand(GameEngine gameEngine)
            : base(gameEngine)
        {
        }

        public override void Execute(string commandName)
        {
            Position positionAfterMovingRight = new Position(GameEngine.Champion.Position.X + 1, GameEngine.Champion.Position.Y);

            bool isFreePosition = GameEngine.IsFreePosition(positionAfterMovingRight);

            if (isFreePosition)
            {
                GameEngine.Champion.Position = positionAfterMovingRight;
            }
            else
            {
                throw new CollisionException("u can't move over wall or champion");
            }
        }
    }
}
