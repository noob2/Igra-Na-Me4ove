namespace IgraNaMe4ove.Engine
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Attributes;
    using GameObjects;
    using GameObjects.Enemies;
    using GameObjects.Items;
    
    public static class MapInitalizer
    {
        private const int WallsCount = 30;
        private const int EnemyCount = 15;
        private const int Items = 5;

        private static readonly Random Random = new Random();

        public static IList<GameObject> PopulateMap()
        {
            IList<GameObject> entities = new List<GameObject>();

            GenerateWalls(entities);
            GenerateEnemy(entities);
            GenerateCollectibles(entities);
            return entities;
        }

        private static void GenerateWalls(IList<GameObject> things)
        {
            int wallsLeft = WallsCount;

            while (wallsLeft > 0)
            {
                Position randomPosition = new Position(Random.Next(Constants.MapWidth), Random.Next(Constants.MapHeight));

                if (IsFreeToSeed(things, randomPosition))
                {
                    things.Add(new Wall(randomPosition));
                    wallsLeft--;
                }
            }
        }

        private static void GenerateEnemy(IList<GameObject> things)
        {
            int enemyLeftToGenerate = EnemyCount;

            var enemyType = Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => t.CustomAttributes.Any(a => a.AttributeType == typeof(EnemyAttribute))).ToArray();

            while (enemyLeftToGenerate > 0)
            {
                Position randomPosition = new Position(Random.Next(Constants.MapWidth), Random.Next(Constants.MapHeight));

                if (IsFreeToSeed(things, randomPosition))
                {
                    Type type = enemyType[Random.Next(0, enemyType.Length)];
                    var enemy = Activator.CreateInstance(type, randomPosition);

                    things.Add((Enemy)enemy);
                    enemyLeftToGenerate--;
                }
            }
        }

        private static void GenerateCollectibles(IList<GameObject> things)
        {
            int itemsLeftToGenerate = Items;

            var itemType = Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => t.CustomAttributes.Any(a => a.AttributeType == typeof(ItemAttribute))).ToArray();

            while (itemsLeftToGenerate > 0)
            {
                Position randomPosition = new Position(Random.Next(Constants.MapWidth), Random.Next(Constants.MapHeight));

                if (IsFreeToSeed(things, randomPosition))
                {
                    Type type = itemType[Random.Next(0, itemType.Length)];
                    var item = Activator.CreateInstance(type, randomPosition);

                    things.Add((Item)item);
                    itemsLeftToGenerate--;
                }
            }
        }

        private static bool IsFreeToSeed(IList<GameObject> things, Position position)
        {
            bool containsSomething = !things.Any(obj => obj.Position.X == position.X && obj.Position.Y == position.Y);
            return containsSomething;
        }
    }
}
