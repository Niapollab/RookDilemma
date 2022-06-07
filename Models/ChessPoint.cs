using System;

namespace RookDilemma.Models
{
    public struct ChessPoint : IEquatable<ChessPoint>
    {
        public int X { get; private set; }

        public int Y { get; private set; }

        public ChessPoint(int y, int x)
        {
            if (y < 0 || y > 7)
                throw new ArgumentException("Param must be positive and less than 8.", nameof(y));

            if (x < 0 || x > 7)
                throw new ArgumentException("Param must be positive and less than 8.", nameof(x));

            Y = y;
            X = x;
        }

        public bool TryAdd(out ChessPoint point, int x, int y)
        {
            point = default;
            int newY = Y + y;
            int newX = X + x;

            if (newY < 0 || newY > 7 || newX < 0 || newX > 7)
                return false;

            point = new ChessPoint(newY, newX);
            return true;
        }

        public override string ToString()
        {
            return $"{(char)('A' + X)}{Y + 1}";
        }

        public override bool Equals(object obj)
        {
            return obj is ChessPoint point && Equals(point);
        }

        public bool Equals(ChessPoint other)
        {
            return X == other.X && Y == other.Y;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }

        public static bool operator ==(ChessPoint left, ChessPoint right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(ChessPoint left, ChessPoint right)
        {
            return !(left == right);
        }
    }
}