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
            object[] insert = new object[4];
            List<string[]> res = bd.SelectPuzzles("");
            foreach (string[] s in res)
            {
                Bitmap MyImage;
                MyImage = new Bitmap(bd.SelectPathByID(s[3]));
                Image im = (Image)MyImage;

                insert[0] = im;
                insert[1] = bd.cutExcessSpace(s[1]);
                insert[2] = bd.cutExcessSpace(s[2]);
                insert[3] = bd.cutExcessSpace(s[4]) + " x " + bd.cutExcessSpace(s[5]);
                dataGridView1.Rows.Add(s);
            }                
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void ReadyPuzzles_Load(object sender, EventArgs e)
        {

        }
    }
}
