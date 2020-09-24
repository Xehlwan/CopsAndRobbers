namespace CopsAndRobbers.Game
{
    public class Item
    {
        public string Name { get; }

        public Item(string name)
        {
            Name = name;
        }

        public Item GetCopy() => new Item(Name);

    }
}