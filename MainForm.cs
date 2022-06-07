using System;
using System.Windows.Forms;
using Voronova.Controllers;
using Voronova.Models;

namespace Voronova
{
    public partial class MainForm : Form
    {
        private readonly StartEndController _startEndController;

        private readonly SolutionController _solutionController;

        private readonly Cell[,] _board;

        public MainForm()
        {
            InitializeComponent();
            _board = InitializeBoard();
            _startEndController = new StartEndController(whiteRookColorRadioButton.Checked ? RookColor.White : RookColor.Black);
            _solutionController = new SolutionController(_startEndController, _board);
        }

        private Cell[,] InitializeBoard()
        {
            return new Cell[,]
            {
                {
                    new Cell(pictureBox40),
                    new Cell(pictureBox39),
                    new Cell(pictureBox38),
                    new Cell(pictureBox37),
                    new Cell(pictureBox36),
                    new Cell(pictureBox35),
                    new Cell(pictureBox34),
                    new Cell(pictureBox33)
                },
                {
                    new Cell(pictureBox48),
                    new Cell(pictureBox47),
                    new Cell(pictureBox46),
                    new Cell(pictureBox45),
                    new Cell(pictureBox44),
                    new Cell(pictureBox43),
                    new Cell(pictureBox42),
                    new Cell(pictureBox41)
                },
                {
                    new Cell(pictureBox56),
                    new Cell(pictureBox55),
                    new Cell(pictureBox54),
                    new Cell(pictureBox53),
                    new Cell(pictureBox52),
                    new Cell(pictureBox51),
                    new Cell(pictureBox50),
                    new Cell(pictureBox49)
                },
                {
                    new Cell(pictureBox64),
                    new Cell(pictureBox63),
                    new Cell(pictureBox62),
                    new Cell(pictureBox61),
                    new Cell(pictureBox60),
                    new Cell(pictureBox59),
                    new Cell(pictureBox58),
                    new Cell(pictureBox57)
                },
                {
                    new Cell(pictureBox24),
                    new Cell(pictureBox23),
                    new Cell(pictureBox22),
                    new Cell(pictureBox21),
                    new Cell(pictureBox20),
                    new Cell(pictureBox19),
                    new Cell(pictureBox18),
                    new Cell(pictureBox17)
                },
                {
                    new Cell(pictureBox32),
                    new Cell(pictureBox31),
                    new Cell(pictureBox30),
                    new Cell(pictureBox29),
                    new Cell(pictureBox28),
                    new Cell(pictureBox27),
                    new Cell(pictureBox26),
                    new Cell(pictureBox25)
                },
                {
                    new Cell(pictureBox16),
                    new Cell(pictureBox15),
                    new Cell(pictureBox14),
                    new Cell(pictureBox13),
                    new Cell(pictureBox12),
                    new Cell(pictureBox11),
                    new Cell(pictureBox10),
                    new Cell(pictureBox9)
                },
                {
                    new Cell(pictureBox1),
                    new Cell(pictureBox2),
                    new Cell(pictureBox3),
                    new Cell(pictureBox4),
                    new Cell(pictureBox5),
                    new Cell(pictureBox6),
                    new Cell(pictureBox7),
                    new Cell(pictureBox8)
                },
            };
        }

        private void CellMouseDowned(object sender, MouseEventArgs e)
        {
            var cell = Cell.FindCell(_board, sender as PictureBox);
            switch (e.Button)
            {
                case MouseButtons.Left:
                    _startEndController.UpdateCell(cell);
                    break;
                case MouseButtons.Right:
                    switch (cell.State)
                    {
                        case CellState.WhitePedestrian:
                            cell.State = CellState.BlackPedestrian;
                            break;
                        case CellState.BlackPedestrian:
                            cell.State = CellState.Empty;
                            break;
                        case CellState.BlackRock:
                        case CellState.WhiteRock:
                        case CellState.EndPoint:
                            break;
                        default:
                            cell.State = CellState.WhitePedestrian;
                            break;
                    }
                    break;
            }
            _solutionController.UpdateSolution();
        }

        private void RookColorChanged(object sender, EventArgs e)
        {
            _startEndController.RookColor = whiteRookColorRadioButton.Checked ? RookColor.White : RookColor.Black;
            _solutionController.UpdateSolution();
        }

        private void CellMouseEntered(object sender, EventArgs e)
        {
            ChessPoint? point = Cell.FindIndex(_board, sender as PictureBox);

            if (point != null)
                currentCellIndexLabel.Text = point.ToString();
        }
    }
}