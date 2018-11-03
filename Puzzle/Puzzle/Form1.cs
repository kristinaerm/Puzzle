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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Bitmap[] bitmap = Section.TriangularSection((Bitmap)pictureBox1.Image, checkBox1.Checked);
            pictureBox2.Image = bitmap[0];
            pictureBox3.Image = bitmap[1];
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
