using System;

namespace Crownfall.Core
{
    [Serializable]
    public readonly struct GridCoordinate : IEquatable<GridCoordinate>
    {
        public readonly int X;
        public readonly int Y;
        public GridCoordinate(int x, int y) { X = x; Y = y; }
        public bool Equals(GridCoordinate other) => X == other.X && Y == other.Y;
        public override bool Equals(object obj) => obj is GridCoordinate other && Equals(other);
        public override int GetHashCode() => (X * 397) ^ Y;
        public override string ToString() => $"({X},{Y})";
    }
}
