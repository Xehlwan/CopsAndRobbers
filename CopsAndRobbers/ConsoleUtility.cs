using System;
using System.Collections.Generic;
using System.Text;

namespace CopsAndRobbers
{
    public static class ConsoleUtility
    {
        public static void ColoredLine(string coloredString)
        {
            ReadOnlySpan<char> formattedText = coloredString.AsSpan();
            int index = -1;
            do
            {
                index = formattedText.IndexOf('[');
                if (index >= 0 && index + 4 < formattedText.Length && formattedText[index + 4] == ']')
                {
                    if (int.TryParse(formattedText.Slice(index + 2, 2), out int color) && color <= 15 && color >= 0)
                    {
                        ConsoleColor consoleColor = (ConsoleColor) color;
                        char c = formattedText[index + 1];
                        if (c == 'f' || c == 'F')
                        {
                            if (index > 0) Console.Write(formattedText.Slice(0, index).ToString());
                            Console.ForegroundColor = consoleColor;
                            formattedText = formattedText.Slice(index + 5);
                        }
                        else if (c == 'b' || c == 'B')
                        {
                            if (index > 0) Console.Write(formattedText.Slice(0, index).ToString());
                            Console.BackgroundColor = consoleColor;
                            formattedText = formattedText.Slice(index + 5);
                        }
                    }
                }
                else
                {
                    Console.Write(formattedText.ToString() + '\n');
                    Console.ResetColor();
                }
            } while (index >= 0);
        }
    }
}
