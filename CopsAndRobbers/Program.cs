using System;
using CopsAndRobbers.Game;

namespace CopsAndRobbers
{
    class Program
    {
        static void Main(string[] args)
        {
            var game = new Engine();
            game.Initialize(150,40,5,5,5);
            while (true)
            {
                game.Tick();
            }
        }
    }
}
