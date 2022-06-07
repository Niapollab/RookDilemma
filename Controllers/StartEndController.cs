using RookDilemma.Models;

namespace RookDilemma.Controllers
{
    public class StartEndController
    {
        private int _state;

        private Cell _rookCell;

        private Cell _endPointCell;

        private RookColor _rookColor;

        public RookColor RookColor
        {
            get => _rookColor;
            set
            {
                if (_rookCell != null)
                    _rookCell.State = value == RookColor.White ? CellState.WhiteRock : CellState.BlackRock;

                _rookColor = value;
            }
        }

        public bool AnswerCanBeBuilded => _state == 2;

        public StartEndController(RookColor rookColor)
        {
            _state = 0;
            _rookCell = null;
            _endPointCell = null;
            _rookColor = rookColor;
        }

        public void UpdateCell(Cell cell)
        {
            switch (_state)
            {
                case 0:
                    cell.State = _rookColor == RookColor.White ? CellState.WhiteRock : CellState.BlackRock;
                    _rookCell = cell;
                    _state = 1;
                    break;
                case 1:
                    if (cell == _rookCell)
                    {
                        cell.State = CellState.Empty;
                        _rookCell = null;
                        _state = 0;
                    }
                    else
                    {
                        cell.State = CellState.EndPoint;
                        _endPointCell = cell;
                        _state = 2;
                    }
                    break;
                case 2:
                    if (cell == _endPointCell)
                    {
                        cell.State = CellState.Empty;
                        _endPointCell = null;
                        _state = 1;
                    }
                    break;
            }
        }
    }
}