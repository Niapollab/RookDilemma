using System;
using System.Collections.Generic;
using RookDilemma.Models;

namespace RookDilemma.Solution
{
    public static class ProblemSolver
    {
        private static readonly IReadOnlyList<(int Y, int X)> RookDirections;

        static ProblemSolver()
        {
            RookDirections = new (int Y, int X)[]
            {
                (0, 1),
                (0, -1),
                (1, 0),
                (-1, 0)
            };
        }

        public static IReadOnlyList<ChessPoint> Solve(ProblemMapEntry[,] map)
        {
            ChessPoint? startPoint = FindStartPoint(map);

            if (startPoint == null)
                throw new ArgumentException("Param must contain a start point.", nameof(map));

            ChessPoint? endPoint = FindEndPoint(map);

            if (endPoint == null)
                throw new ArgumentException("Param must contain a end point.", nameof(map));

            (ChessPoint? From, int Deep)[,] waveAlgorithmMap = BuildWaveAlgorithmMap(startPoint.Value, map);

            if (waveAlgorithmMap[endPoint.Value.Y, endPoint.Value.X].Deep < 1)
                throw new ProblemSolverException("Unable to achieve end point from start point.");

            return RestoreFullTrace(RestorePath(waveAlgorithmMap, endPoint.Value));
        }

        public static IReadOnlyList<ChessPoint> RestoreFullTrace(IReadOnlyList<ChessPoint> solution)
        {
            _ = solution ?? throw new ArgumentNullException(nameof(solution));

            var fullTrace = new List<ChessPoint>();

            for (var i = 0; i < solution.Count - 1; ++i)
            {
                ChessPoint from = solution[i];
                ChessPoint to = solution[i + 1];
                (int Y, int X)? direction = FindDirection(from, to);

                if (direction == null)
                    throw new ArgumentException("Points in solution has unknown direction.", nameof(solution));

                while (from != to)
                {
                    fullTrace.Add(from);
                    from.TryAdd(out var nextPoint, direction.Value.X, direction.Value.Y);
                    from = nextPoint;
                }
            }

            fullTrace.Add(solution[^1]);

            return fullTrace;
        }

        private static (int Y, int X)? FindDirection(ChessPoint from, ChessPoint to)
        {
            int deltaY = to.Y - from.Y;
            int deltaX = to.X - from.X;

            if (!((deltaX == 0) ^ (deltaY == 0)))
                return null;

            if (deltaX == 0)
                return deltaY > 0 ? (1, 0) : (-1, 0);

            return deltaX > 0 ? (0, 1) : (0, -1);
        }

        private static IReadOnlyList<ChessPoint> RestorePath((ChessPoint? From, int Deep)[,] waveAlgorithmMap, ChessPoint endPoint)
        {
            var path = new List<ChessPoint>();

            ChessPoint? currentPoint = endPoint;
            while (currentPoint != null)
            {
                path.Add(currentPoint.Value);
                currentPoint = waveAlgorithmMap[currentPoint.Value.Y, currentPoint.Value.X].From;
            }

            path.Reverse();

            return path;
        }

        private static (ChessPoint? From, int Deep)[,] BuildWaveAlgorithmMap(ChessPoint startPoint, ProblemMapEntry[,] map)
        {
            var waveAlgorithmMap = new (ChessPoint? From, int Deep)[map.GetLength(0), map.GetLength(1)];
            var pointsQueue = new Queue<ChessPoint>();

            waveAlgorithmMap[startPoint.Y, startPoint.X] = (null, 1);
            pointsQueue.Enqueue(startPoint);

            while (pointsQueue.Count > 0)
            {
                ChessPoint currentPoint = pointsQueue.Dequeue();

                foreach ((int Y, int X) direction in RookDirections)
                    MakeWave(direction, currentPoint);
            }

            return waveAlgorithmMap;

            void MakeWave((int Y, int X) direction, ChessPoint startPoint)
            {
                ChessPoint currentPoint = startPoint;
                while (currentPoint.TryAdd(out var nextPoint, direction.X, direction.Y))
                {
                    if (map[nextPoint.Y, nextPoint.X] == ProblemMapEntry.Friend)
                        return;

                    if (waveAlgorithmMap[nextPoint.Y, nextPoint.X].Deep > 0 && waveAlgorithmMap[nextPoint.Y, nextPoint.X].Deep <= waveAlgorithmMap[startPoint.Y, startPoint.X].Deep)
                        return;

                    waveAlgorithmMap[nextPoint.Y, nextPoint.X].From = startPoint;
                    waveAlgorithmMap[nextPoint.Y, nextPoint.X].Deep = waveAlgorithmMap[startPoint.Y, startPoint.X].Deep + 1;
                    pointsQueue.Enqueue(nextPoint);

                    if (map[nextPoint.Y, nextPoint.X] == ProblemMapEntry.Enemy)
                        return;

                    currentPoint = nextPoint;
                }
            }
        }

        private static ChessPoint? FindStartPoint(ProblemMapEntry[,] map)
        {
            for (var i = 0; i < map.GetLength(0); ++i)
                for (var j = 0; j < map.GetLength(1); ++j)
                    if (map[i, j] == ProblemMapEntry.StartPoint)
                        return new ChessPoint(i, j);

            return null;
        }

        private static ChessPoint? FindEndPoint(ProblemMapEntry[,] map)
        {
            for (var i = 0; i < map.GetLength(0); ++i)
                for (var j = 0; j < map.GetLength(1); ++j)
                    if (map[i, j] == ProblemMapEntry.EndPoint)
                        return new ChessPoint(i, j);

            return null;
        }
    }
}