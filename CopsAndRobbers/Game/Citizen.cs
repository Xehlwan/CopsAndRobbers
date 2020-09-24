using System;

namespace CopsAndRobbers.Game
{
    class Citizen : Person
    {
        public override char Symbol => 'C';

        public override ConsoleColor SymbolColor => ConsoleColor.White;
    }
}
