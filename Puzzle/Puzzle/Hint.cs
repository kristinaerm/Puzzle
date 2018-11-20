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
    public partial class Hint : Form
    {
        public Hint()
        {
            InitializeComponent();
        }

        public Hint(PictureBox hint)
        {
            InitializeComponent();
            this.Size = new Size(hint.Width + 50, hint.Height + 80);
            this.Controls.Add(hint);
        }

        private void Hint_Load(object sender, EventArgs e)
        {

        }
    }
}
