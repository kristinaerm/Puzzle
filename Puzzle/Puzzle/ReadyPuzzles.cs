using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Puzzle
{
    public partial class ReadyPuzzles : Form
    {
        private ConnDatabase bd = new ConnDatabase();
        public ReadyPuzzles()
        {
            InitializeComponent();
            dataGridView1.RowTemplate.Height = 150;
            List<string[]> res = bd.selectPuzzlesByComplexity("");
            int x = 0;
            foreach (string[] s in res)
            {
                dataGridView1.Rows.Add();
                
                Bitmap MyImage;
                MyImage = new Bitmap(bd.selectPathByIdPicture(s[3]));
                Bitmap image2 = new Bitmap(MyImage, 200, 150);
                dataGridView1[0, x].Value = image2;
                dataGridView1[1, x].Value = bd.cutExcessSpace(s[1]);
                dataGridView1[2, x].Value = bd.cutExcessSpace(s[2]);
                dataGridView1[3, x].Value = bd.cutExcessSpace(s[4]) + " x " + bd.cutExcessSpace(s[5]);
                x++;
            }                
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            Point xy = dataGridView1.CurrentCellAddress;
            int x = xy.X;
            int y = xy.Y;
            if (x == 4)
            {
                List<string[]> res = bd.selectPuzzlesByComplexity("");
                List<string> id_piece = new List<string>();
                if (y < res.Count)
                {
                    string id = res[y][0];
                    
                    id_piece = bd.selectIdPiece(id);
                    bd.deleteSaveByIdPuzzle(id);
                    for(int i=0;i<id_piece.Count;i++)
                    bd.deletePiecePuzzleByIdPuzzleAndOrIdPuzzle(id, id_piece[i]);
                    bd.deleteGameByIdPuzzle(id);
                    bd.deletePuzzle(id);
                    dataGridView1.Rows.RemoveAt(y);
                }
            }
        }

        private void ReadyPuzzles_Load(object sender, EventArgs e)
        {

        }
    }
}
