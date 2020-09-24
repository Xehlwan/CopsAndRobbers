using System;
using System.Collections.Generic;
using System.Text;

namespace CopsAndRobbers.Game
{
    public class Renderer
    {
        public int Width { get; }
        public int Height { get; }

        public Renderer(int width, int height)
        {
            Width = width;
            Height = height;
            if (width < Console.WindowWidth) Console.WindowWidth = width;
            if (height < Console.WindowHeight) Console.WindowHeight = height;
            Console.SetBufferSize(width, height);
            Console.SetWindowSize(width, height);
            Console.CursorVisible = false;
        }

        public void RenderField(PlayField field)
        {
            Console.Clear();
            foreach (var person in field.GetPeople())
            {

                Console.SetCursorPosition(person.Position.X, person.Position.Y);
                if (Console.ForegroundColor != person.SymbolColor) Console.ForegroundColor = person.SymbolColor;
                Console.Write(person.Symbol);
            }

            Console.ForegroundColor = ConsoleColor.White;
        }

        public void RenderTextField(TextField field, bool topAligned, ConsoleColor borderColor = ConsoleColor.Gray)
        {
            int top, bottom;
            if (topAligned)
            {
                top = 0;
                bottom = field.Count + 2;
            }
            else
            { 
                bottom = Height - 1;
                top = bottom - field.Count - 2;
            }
            DrawBorder(top, bottom, borderColor);
            var text = field.GetEnumerator();
            text.MoveNext();
            for (int i = 0; i < field.Count; i++)
            {
                Console.SetCursorPosition(1, top + 1 + i);
                if (Console.ForegroundColor != text.Current.color) Console.ForegroundColor = text.Current.color;
                Console.Write(text.Current.text);
                text.MoveNext();
            }
            text.Dispose();

        }

        private void DrawBorder(int top, int bottom, ConsoleColor borderColor)
        {
            Console.ForegroundColor = borderColor;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(0, top);

            int height = bottom - top;
            int realWidth = Width + 1;
            Span<char> boxLine = new char[realWidth * height];

            for (int i = 0; i < boxLine.Length; i++)
            {
                if (i == 0)
                    boxLine[i] = '╔';
                else if (i == Width - 1)
                    boxLine[i] = '╗';
                else if (i == (Height - 1) * realWidth)
                    boxLine[i] = '╚';
                else if (i == realWidth * Height - 2)
                    boxLine[i] = '╝';
                else if(i < Width || (i > (height - 1) * realWidth && i < realWidth * Height - 2))
                    boxLine[i] = '═';
                else if (i % realWidth == 0 || (i + 2) % realWidth == 0)
                    boxLine[i] = '║';
                else if ((i + 1) % realWidth == 0)
                    boxLine[i] = '\n';
                else
                    boxLine[i] = '\uFEFF';
            }
            Console.Write(boxLine.ToString());
        }
    }
}
