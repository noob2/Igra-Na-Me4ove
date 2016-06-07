namespace IgraNaMe4ove.Engine
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Exceptions;
    using Factories;
    using GameObjects;
    using GameObjects.Characters;
    using GameObjects.Enemies;
    using GameObjects.Items;
    using Interfaces;

    public class GameEngine
    {
        public readonly IRenderer Renderer;
        public readonly IInputHandler InputHandler;
        public readonly IController Controller;

        private static readonly Random Random = new Random();

        private IList<GameObject> objectsOnMap = new List<GameObject>();
        private Character champion;
        
        private char[,] oldMapArray = new char[Constants.MapWidth, Constants.MapHeight];
        private char[,] mapArray = new char[Constants.MapWidth, Constants.MapHeight];

        public GameEngine(Character champion, IRenderer renderer, IInputHandler inputHandler, IController controller, CommandFactory commandFactory)
        {
            this.objectsOnMap = MapInitalizer.PopulateMap();
            this.champion = champion;
            this.Renderer = renderer;
            this.InputHandler = inputHandler;
            this.Controller = controller;
            this.CommandFactory = commandFactory;
        }

        public CommandFactory CommandFactory { get; private set; }

        public char[,] OldMapArray
        {
            get
            {
                return this.oldMapArray;
            }
        }

        public Character Champion
        {
            get
            {
                return this.champion;
            }
        }

        public IList<GameObject> ObjectsOnMap
        {
            get
            {
                return this.objectsOnMap;
            }
        }

        public void Run()
        {
            this.SelectCharacter();
            this.DrawMap();

            while (true)
            {
                this.SaveToOldArray();

                char keyPressed = this.Controller.ReadKey();

                Position errorMessegePostion = new Position(1, Constants.MapHeight + 1);

                try
                {
                    this.TakeAction(keyPressed);
                }
                catch (InvalidKeyException ex)
                {
                    this.Renderer.WriteAt(errorMessegePostion, ex.Message);
                }
                catch (CollisionException ex)
                {
                    this.Renderer.WriteAt(errorMessegePostion, ex.Message);
                }
                catch (ItemNotFoundException ex)
                {
                    this.Renderer.WriteAt(errorMessegePostion, ex.Message);
                }

                this.SaveToNewArray();

                this.DrawChanges();
            }
        }

        public void DrawMap()
        {
            this.Renderer.Clear();

            foreach (var obj in this.objectsOnMap)
            {
                this.Renderer.WriteAt(new Position(obj.Position.X, obj.Position.Y), obj.Symbol, obj.Colour);
                this.mapArray[obj.Position.X, obj.Position.Y] = obj.Symbol;
            }

            this.Renderer.WriteAt(new Position(this.champion.Position.X, this.champion.Position.Y), this.champion.Symbol, this.champion.Colour);
            this.mapArray[this.champion.Position.X, this.champion.Position.Y] = this.champion.Symbol;

            this.RedrawChampionStatusOnMap();
        }

        public void RedrawEnemyStatusOnMap(Enemy enemy)
        {
            string spacesToWriteBeforeStatus = "                         ";

            List<string> enemyStatus = enemy.Status();
            for (int i = 0; i < enemyStatus.Count; i++)
            {
                this.Renderer.WriteAt(new Position(Constants.MapWidth + 1, i + 6), spacesToWriteBeforeStatus);
                this.Renderer.WriteAt(new Position(Constants.MapWidth + 1, i + 6), enemyStatus[i]);
            }
        }

        public bool IsEnemyInRange(Enemy enemy)
        {
            if (Math.Abs(enemy.Position.X - this.champion.Position.X) <= this.champion.Range && Math.Abs(enemy.Position.Y - this.champion.Position.Y) <= this.champion.Range)
            {
                return true;
            }

            return false;
        }

        public bool IsFreePosition(Position position)
        {
            if (position.X < 0 || position.X >= Constants.MapWidth || position.Y < 0 || position.Y >= Constants.MapHeight)
            {
                return false;
            }

            bool containsGameObject = !this.objectsOnMap.Any(obj => obj.Position.X == position.X && obj.Position.Y == position.Y && !(obj is Item));

            if (containsGameObject)
            {
                return true;
            }

            return false;
        }

        private void EnemiesTurn()
        {
            foreach (var enemy in this.objectsOnMap)
            {
                if (enemy is Enemy)
                {
                    int direction = Random.Next(4);

                    Position positionAfterMovingDown = new Position(enemy.Position.X + 1, enemy.Position.Y);
                    Position positionAfterMovingUp = new Position(enemy.Position.X - 1, enemy.Position.Y);
                    Position positionAfterMovingRight = new Position(enemy.Position.X, enemy.Position.Y + 1);
                    Position positionAfterMovingLeft = new Position(enemy.Position.X, enemy.Position.Y - 1);

                    if (direction == 0 && this.IsFreePosition(positionAfterMovingDown))
                    {
                        enemy.Position = positionAfterMovingDown;
                    }
                    else if (direction == 1 && this.IsFreePosition(positionAfterMovingUp))
                    {
                        enemy.Position = positionAfterMovingUp;
                    }
                    else if (direction == 2 && this.IsFreePosition(positionAfterMovingRight))
                    {
                        enemy.Position = positionAfterMovingRight;
                    }
                    else if (direction == 3 && this.IsFreePosition(positionAfterMovingLeft))
                    {
                        enemy.Position = positionAfterMovingLeft;
                    }

                    if (this.IsChampionInRange(enemy as Enemy))
                    {
                        (enemy as Enemy).Attack(this.champion);
                        this.RedrawChampionStatusOnMap();
                    }
                }
            }
        }

        private bool IsChampionInRange(Enemy enemy)
        {
            if (Math.Abs(enemy.Position.X - this.champion.Position.X) <= enemy.Range && Math.Abs(enemy.Position.Y - this.champion.Position.Y) <= enemy.Range)
            {
                return true;
            }

            return false;
        }

        private void TakeAction(char keyPressed)
        {
            const string CommandSuffix = "Command";
            string commandName = string.Empty;

            switch (keyPressed)
            {
                case 'U':
                    commandName = "MoveUp";
                    break;

                case 'D':
                    commandName = "MoveDown";
                    break;

                case 'R':
                    commandName = "MoveRight";
                    break;

                case 'L':
                    commandName = "MoveLeft";
                    break;

                case 'S':
                    commandName = "MarkTargetAndAttack";
                    break;

                case 'H':
                    commandName = "ExecuteHelp";
                    break;

                case 'I':
                    commandName = "OpenInventory";
                    break;

                case 'C':
                    commandName = "CollectItem";
                    break;

                default:
                    throw new InvalidKeyException("Invalid key.");
            }

            commandName += CommandSuffix;
            var command = this.CommandFactory.CreateCommand(commandName, this);
            command.Execute(commandName);

            this.EnemiesTurn();
        }

        private void SelectCharacter()
        {
            Position randomPosition;
            do
            {
                randomPosition = new Position(Random.Next(Constants.MapWidth), Random.Next(Constants.MapHeight));
            }
            while (!this.IsFreePosition(randomPosition));

            this.champion.Position = randomPosition;

            while (true)
            {
                this.Renderer.Clear();
                this.Renderer.WriteAt(new Position(0, 0), "Please select champion :");
                this.Renderer.WriteAt(new Position(0, 1), "'A' for Ashe");
                this.Renderer.WriteAt(new Position(0, 2), "'K' for Kassadin");
                this.Renderer.WriteAt(new Position(0, 3), "'G' for Garen");
                this.Renderer.WriteAt(new Position(0, 4), "'L' for Lissandra");
                this.Renderer.WriteAt(new Position(25, 3), "'H' for help Menu");

                char selector = this.Controller.ReadKey();

                switch (selector)
                {
                    case 'A':
                        this.champion.Race = CharacterRace.Ashe;
                        this.champion.SetPlayerStats();
                        return;

                    case 'K':
                        this.champion.Race = CharacterRace.Kassadin;
                        this.champion.SetPlayerStats();
                        return;

                    case 'G':
                        this.champion.Race = CharacterRace.Garen;
                        this.champion.SetPlayerStats();
                        return;

                    case 'L':
                        this.champion.Race = CharacterRace.Lissandra;
                        this.champion.SetPlayerStats();
                        return;

                    case 'H':
                        string helpCommand = "ExecuteHelpCommand";
                        this.CommandFactory.CreateCommand(helpCommand, this).Execute(helpCommand);
                        break;
                }
            }
        }
        
        private void DrawChanges()
        {
            for (int y = 0; y < Constants.MapHeight; y++)
            {
                for (int x = 0; x < Constants.MapWidth; x++)
                {
                    if (this.mapArray[x, y] != this.oldMapArray[x, y])
                    {
                        GameObject obj = this.objectsOnMap.FirstOrDefault(o => o.Position.X == x && o.Position.Y == y);

                        if (obj != null)
                        {
                            this.Renderer.WriteAt(new Position(x, y), this.mapArray[x, y], obj.Colour);
                        }
                        else
                        {
                            this.Renderer.WriteAt(new Position(x, y), " ");
                        }
                    }
                }
            }

            this.Renderer.WriteAt(new Position(this.champion.Position.X, this.champion.Position.Y), this.champion.Symbol, this.champion.Colour);
        }

        private void SaveToNewArray()
        {
            // first empty the array
            for (int y = 0; y < Constants.MapHeight; y++)
            {
                for (int x = 0; x < Constants.MapWidth; x++)
                {
                    this.mapArray[x, y] = '\0';
                }
            }

            foreach (var obj in this.objectsOnMap)
            {
                this.mapArray[obj.Position.X, obj.Position.Y] = obj.Symbol;
            }

            this.mapArray[this.champion.Position.X, this.champion.Position.Y] = this.champion.Symbol;
        }

        private void SaveToOldArray()
        {
            for (int y = 0; y < Constants.MapHeight; y++)
            {
                for (int x = 0; x < Constants.MapWidth; x++)
                {
                    this.oldMapArray[x, y] = this.mapArray[x, y];
                }
            }
        }

        /// <summary>
        /// clears the place where it redraws the champion status on the map, and then redraw it
        /// </summary>
        private void RedrawChampionStatusOnMap()
        {
            IList<string> championStatus = this.champion.Status();

            string spacesToWriteBeforeStatus = "                    ";
            for (int i = 0; i < championStatus.Count; i++)
            {
                this.Renderer.WriteAt(new Position(Constants.MapWidth + 1, i), spacesToWriteBeforeStatus);
                this.Renderer.WriteAt(new Position(Constants.MapWidth + 1, i), championStatus[i]);
            }
        }
    }
}