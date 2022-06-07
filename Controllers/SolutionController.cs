using System;
using System.Collections.Generic;
using RookDilemma.Models;
using RookDilemma.Solution;

namespace RookDilemma.Controllers
{
    public class SolutionController
    {
        private readonly StartEndController _startEndController;

        private readonly Cell[,] _board;

        public SolutionController(StartEndController startEndController, Cell[,] board)
        {
            _startEndController = startEndController ?? throw new ArgumentNullException(nameof(startEndController));
            _board = board ?? throw new ArgumentNullException(nameof(board));
        }

        public void UpdateSolution()
        {
            ClearPreviewAnswer();

            if (!_startEndController.AnswerCanBeBuilded)
                return;

            ProblemMapEntry[,] problemMap = BuildProblemMap();
            try
            {
                IReadOnlyList<ChessPoint> solution = ProblemSolver.Solve(problemMap);

                foreach (ChessPoint point in solution)
                    _board[point.Y, point.X].AnswerCellState = AnswerCellState.IncludeCell;
            } catch (ProblemSolverException)
            {
            }
        }

        private ProblemMapEntry[,] BuildProblemMap()
        {
            var map = new ProblemMapEntry[_board.GetLength(0), _board.GetLength(1)];

            for (var i = 0; i < _board.GetLength(0); ++i)
                for (var j = 0; j < _board.GetLength(1); ++j)
                    switch (_board[i, j].State)
                    {
                        case CellState.Empty:
                            map[i, j] = ProblemMapEntry.EmptyCell;
                            break;
                        case CellState.WhitePedestrian:
                            map[i, j] = _startEndController.RookColor == RookColor.White ? ProblemMapEntry.Friend : ProblemMapEntry.Enemy;
                            break;
                        case CellState.BlackPedestrian:
                            map[i, j] = _startEndController.RookColor == RookColor.White ? ProblemMapEntry.Enemy : ProblemMapEntry.Friend;
                            break;
                        case CellState.WhiteRock:
                        case CellState.BlackRock:
                            map[i, j] = ProblemMapEntry.StartPoint;
                            break;
                        case CellState.EndPoint:
                            map[i, j] = ProblemMapEntry.EndPoint;
                            break;
                    }

            return map;
        }

        private void ClearPreviewAnswer()
        {
            for (var i = 0; i < _board.GetLength(0); ++i)
                for (var j = 0; j < _board.GetLength(1); ++j)
                    _board[i, j].AnswerCellState = AnswerCellState.NotIncludeCell;
        }
    }
}