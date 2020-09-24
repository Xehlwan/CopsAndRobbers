using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace CopsAndRobbers.Game
{
    public class TextField : IEnumerable<(string text, ConsoleColor color)>
    {
        private readonly List<(string text, ConsoleColor color)> textLines = new List<(string, ConsoleColor)>();

        public bool Border { get; set; }
        public int Count => textLines.Count;
        public int Length => textLines.Count;
        public void Clear() => textLines.Clear();
        public void Add(string text, ConsoleColor color = ConsoleColor.White) => textLines.Add((text, color));

        public IEnumerator<(string text, ConsoleColor color)> GetEnumerator()
        {
            foreach (var line in textLines)
            {
                yield return (line.text, line.color);
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
