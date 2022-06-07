using System.Drawing;
using System.Windows.Forms;

namespace Voronova
{
    public partial class MainForm : Form
    {
        private readonly PictureBox[,] _board;

        public MainForm()
        {
            InitializeComponent();
            _board = InitializeBoard();
        }

        private PictureBox[,] InitializeBoard()
        {
            return new PictureBox[,]
            {
                {
                    pictureBox40,
                    pictureBox39,
                    pictureBox38,
                    pictureBox37,
                    pictureBox36,
                    pictureBox35,
                    pictureBox34,
                    pictureBox33
                },
                {
                    pictureBox48,
                    pictureBox47,
                    pictureBox46,
                    pictureBox45,
                    pictureBox44,
                    pictureBox43,
                    pictureBox42,
                    pictureBox41
                },
                {
                    pictureBox56,
                    pictureBox55,
                    pictureBox54,
                    pictureBox53,
                    pictureBox52,
                    pictureBox51,
                    pictureBox50,
                    pictureBox49
                },
                {
                    pictureBox64,
                    pictureBox63,
                    pictureBox62,
                    pictureBox61,
                    pictureBox60,
                    pictureBox59,
                    pictureBox58,
                    pictureBox57
                },
                {
                    pictureBox24,
                    pictureBox23,
                    pictureBox22,
                    pictureBox21,
                    pictureBox20,
                    pictureBox19,
                    pictureBox18,
                    pictureBox17
                },
                {
                    pictureBox32,
                    pictureBox31,
                    pictureBox30,
                    pictureBox29,
                    pictureBox28,
                    pictureBox27,
                    pictureBox26,
                    pictureBox25
                },
                {
                    pictureBox16,
                    pictureBox15,
                    pictureBox14,
                    pictureBox13,
                    pictureBox12,
                    pictureBox11,
                    pictureBox10,
                    pictureBox9
                },
                {
                    pictureBox1,
                    pictureBox2,
                    pictureBox3,
                    pictureBox4,
                    pictureBox5,
                    pictureBox6,
                    pictureBox7,
                    pictureBox8
                },
            };
        }

        private void CellMouseDowned(object sender, MouseEventArgs e)
        {

        }
    }
}