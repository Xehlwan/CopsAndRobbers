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
            Console.SetCursorPosition(0,0);
            Console.WriteLine($"Robberies: {field.Robberies}   Arrests: {field.Arrests}   Prisoners: {field.PeopleInPrison}");
            if (field.PeopleInPrison > 0) 
                Console.WriteLine($"Next release in {field.NextRelease} ticks.");
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
    }
}
