using System;
using System.Threading;
using CopsAndRobbers.Game;

namespace CopsAndRobbers
{
    class Program
    {
        static void Main(string[] args)
        {
            var game = new Engine();

            ConsoleUtility.ColoredLine("[F09]Cops [F15]& [F12] Robbers!\n\n");
            Console.WriteLine();
            int citizens = GetValue("How many citizens? ");
            int cops = GetValue("How many cops? ");
            int thieves = GetValue("How many thieves? ");
            
            game.Initialize(150,40, citizens, cops, thieves);

            bool running = true;
            while (running)
            {
                game.Tick();
                Thread.Sleep(50);
                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(true).Key;

                    switch (key)
                    {
                        case ConsoleKey.Escape:
                            running = false;

                            break;
                        case ConsoleKey.Enter:
                            game.Initialize(150, 40, 30, 10, 20);
                            break;
                    }
                }
            }
        }

        private static int GetValue(string prompt)
        {
            int value = 0;
            Console.Write(prompt);
            Console.CursorVisible = true;
            if (!int.TryParse(Console.ReadLine(), out value) || value < 0)
            {
                Console.CursorTop--;
                Console.CursorLeft = 0;
                Console.Write(prompt);
            }

            return value;
        }
    }
}
