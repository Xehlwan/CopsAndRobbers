using System;
using System.Text;

namespace CopsAndRobbers.Game
{
    public class Police : Person
    {
        public override char Symbol => 'P';

        public override ConsoleColor SymbolColor => ConsoleColor.Blue;

        public override (string, CollisionEvent) TakeItem(Person victim)
        {
            if (victim is Thief && victim.Inventory.Count > 0)
            {
                var sb = new StringBuilder();
                const string msgPrefix = "Police confiscated:";
                sb.Append(msgPrefix);
                foreach (Item item in victim.Inventory)
                {
                    Inventory.Add(item);
                    sb.Append($" {item.Name},");
                }
                victim.Inventory.Clear();

                sb.Remove(sb.Length - 1, 1);
                sb.Append(" from thief, and sent them to prison!");

                return (sb.ToString(), CollisionEvent.Arrest);
            }

            return (string.Empty, CollisionEvent.NoEvent);
        }
    }
}
