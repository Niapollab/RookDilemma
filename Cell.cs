using System;
using System.Drawing;
using System.Windows.Forms;

namespace Voronova
{
    public class Cell
    {
        private readonly PictureBox _texture;

        private CellState _state;

        public CellState State
        {
            get => _state;
            set
            {
                switch (value)
                {
                    case CellState.Empty:
                        _texture.Image = null;
                        break;
                    case CellState.WhitePedestrian:
                        _texture.Image = Resources.PedestrianWhite;
                        break;
                    case CellState.BlackPedestrian:
                        _texture.Image = Resources.PedestrianBlack;
                        break;
                    case CellState.WhiteRock:
                        _texture.Image = Resources.RookWhite;
                        break;
                    case CellState.BlackRock:
                        _texture.Image = Resources.RookBlack;
                        break;
                    case CellState.EndPoint:
                        _texture.Image = Resources.EndPoint;
                        break;
                }
                _state = value;
            }
        }

        public Cell(PictureBox texture)
        {
            _state = CellState.Empty;
            _texture = texture ?? throw new ArgumentNullException(nameof(texture));
        }

        public static Cell Find(Cell[,] cells, PictureBox texture)
        {
            _ = cells ?? throw new ArgumentNullException(nameof(cells));
            _ = texture ?? throw new ArgumentNullException(nameof(texture));

            for (var i = 0; i < cells.GetLength(0); ++i)
                for (var j = 0; j < cells.GetLength(1); ++j)
                    if (cells[i, j]._texture == texture)
                        return cells[i, j];

            return null;
        }
    }
}