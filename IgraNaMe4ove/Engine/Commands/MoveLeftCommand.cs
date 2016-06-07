namespace IgraNaMe4ove.Engine.Commands
{
    using Attributes;
    using Exceptions;

    [Command]
    public class MoveLeftCommand : Command
    {
        public MoveLeftCommand(GameEngine gameEngine)
            : base(gameEngine)
        {
        }

        public override void Execute(string commandName)
        {
            Position positionAfterMovingLeft = new Position(GameEngine.Champion.Position.X - 1, GameEngine.Champion.Position.Y);

            bool isFreePosition = GameEngine.IsFreePosition(positionAfterMovingLeft);

            if (isFreePosition)
            {
                GameEngine.Champion.Position = positionAfterMovingLeft;
            }
            else
            {
                throw new CollisionException("u can't move over wall or champion");
            }
        }
    }
}
