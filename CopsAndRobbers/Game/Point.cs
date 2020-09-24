using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.VisualBasic.CompilerServices;

namespace CopsAndRobbers.Game
{
    public struct Point : IComparable<Point>, IEquatable<Point>
    {
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; }
        public int Y { get; }

        public int CompareTo(Point other)
        {
            int xComparison = X.CompareTo(other.X);

            if (xComparison != 0) return xComparison;

            return Y.CompareTo(other.Y);
        }

        public static implicit operator (int x, int y)(Point p) => (p.X, p.Y);
        public static implicit operator Point((int x, int y) tuple) => new Point(tuple.x, tuple.y);
        public static bool operator ==(Point a, Point b) => a.Equals(b);

        public static bool operator !=(Point a, Point b) => !(a == b);

        public bool Equals(Point other) => X == other.X && Y == other.Y;

        public override bool Equals(object obj) => obj is Point other && Equals(other);

        public override int GetHashCode() => HashCode.Combine(X, Y);
    }
}
