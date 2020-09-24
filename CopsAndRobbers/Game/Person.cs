using System;
using System.Collections.Generic;

namespace CopsAndRobbers.Game
{
    public abstract class Person
    {
        protected Random Rng;
        public List<Item> Inventory { get; } = new List<Item>();

        public Point Position { get; set; }

        public Direction Direction { get; set; }

        public abstract char Symbol { get; }
        public abstract ConsoleColor SymbolColor { get; }

        public Person() => Rng = new Random();

        public Person(int seed) => Rng = new Random(seed);

        public virtual (string msg, ConsoleColor color) TakeItem(Person victim) => (null, ConsoleColor.Black);

        public virtual void Move()
        {
            Direction = (Direction) Rng.Next(1, 8 + 1);
            Position = Direction switch
            {
                Direction.Up => (Position.X, Position.Y - 1),
                Direction.Down => (Position.X, Position.Y + 1),
                Direction.Left => (Position.X - 1, Position.Y ),
                Direction.Right => (Position.X + 1, Position.Y),
                Direction.UpLeft => (Position.X - 1, Position.Y - 1),
                Direction.UpRight => (Position.X + 1, Position.Y - 1),
                Direction.DownLeft => (Position.X - 1, Position.Y + 1),
                Direction.DownRight => (Position.X + 1, Position.Y + 1),
                _ => Position
            };
        }

        public void RandomizePosition(int width, int height)
        {
            Position = (Rng.Next(width), Rng.Next(height));
        }
    }
}
