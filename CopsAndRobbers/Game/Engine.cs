using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace CopsAndRobbers.Game
{
    public class Engine
    {
        private const int robberySentence = 300;
        private PlayField field;
        private Renderer renderer;

        private readonly List<Item> itemTypes = new List<Item>();

        public Engine()
        {
            Console.OutputEncoding = Encoding.Unicode;
            itemTypes.Add(new Item("Keys"));
            itemTypes.Add(new Item("Mobile"));
            itemTypes.Add(new Item("Money"));
            itemTypes.Add(new Item("Watch"));
        }

        public void Initialize(int width, int height, int citizens, int cops, int thieves)
        {
            field = new PlayField(width, height);
            renderer = new Renderer(width, height);
            for (int i = 0; i < citizens; i++)
            {
                var citizen = new Citizen();
                foreach (Item itemType in itemTypes)
                {
                    citizen.Inventory.Add(itemType.GetCopy());
                }
                field.AddPerson(citizen);
            }

            for (int i = 0; i < cops; i++)
            {
                field.AddPerson(new Police());
            }

            for (int i = 0; i < thieves; i++)
            {
                field.AddPerson(new Thief());
            }
        }

        public void Tick()
        {
            field.ReleasePrisoners();
            field.MovePeople();
            renderer.RenderField(field);
            bool pause = TestCollisions();

            if (pause) Thread.Sleep(1000);
        }

        private bool TestCollisions()
        {
            var pause = false;
            var collisions = field.GetCollisions();
            foreach (HashSet<Person> collision in collisions)
            {
                if (HandleCollision(collision))
                {
                    pause = true;
                    var position = collision.First().Position;
                    Console.SetCursorPosition(position.X, position.Y);
                    if (Console.ForegroundColor != ConsoleColor.White) Console.ForegroundColor = ConsoleColor.White;
                    if (Console.BackgroundColor != ConsoleColor.DarkRed) Console.BackgroundColor = ConsoleColor.DarkRed;
                    Console.Write('X');
                }
                
            }

            if (Console.ForegroundColor != ConsoleColor.White) Console.ForegroundColor = ConsoleColor.White;
            if (Console.BackgroundColor != ConsoleColor.Black) Console.BackgroundColor = ConsoleColor.Black;
            return pause;
        }

        private bool HandleCollision(HashSet<Person> collision)
        {
            TextField textField = new TextField();
            foreach (Person person in collision)
            {
                foreach (Person victim in collision)
                {
                    if (person.Equals(victim)) continue;
                    var result = person.TakeItem(victim);
                    switch (result.collisionEvent)
                    {
                        case CollisionEvent.Robbery:
                            field.Robberies++;
                            textField.Add(result.msg, ConsoleColor.Red);
                            break;
                        case CollisionEvent.Arrest:
                            field.PutInPrison(victim, robberySentence);
                            field.Arrests++;
                            textField.Add(result.msg, ConsoleColor.Blue);

                            break;
                        case CollisionEvent.FailedRobbery:
                            textField.Add(result.msg, ConsoleColor.White);
                            break;
                    }
                }
            }

            if (textField.Count > 0)
            {
                renderer.RenderTextField(textField, false);

                return true;
            }

            return false;
        }
    }
}
