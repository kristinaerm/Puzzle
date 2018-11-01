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
    public partial class Recorde : Form
    {
        public Recorde()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
            ConnDatabase bd = new ConnDatabase();
            List<string[]> res = new List<string[]>();

            if (comboBox1.Text == "По времени")
            {
                res.Clear();
                res = bd.SelectResultOfGame("По времени");

                foreach (string[] s in res)
                    dataGridView1.Rows.Add(s);
            }
            else
            {
                res.Clear();
                res = bd.SelectResultOfGame("По очкам");

                foreach (string[] s in res)
                    dataGridView1.Rows.Add(s);
            }
        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Recorde_Load(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ConnDatabase bd = new ConnDatabase();
            List<string[]> res = new List<string[]>();

            if (comboBox1.Text == "По времени")
            {
                res.Clear();
                res = bd.SelectResultOfGame("По времени");

                foreach (string[] s in res)
                    dataGridView1.Rows.Add(s);
            }
            else
            {
                res.Clear();
                res = bd.SelectResultOfGame("По очкам");

                foreach (string[] s in res)
                    dataGridView1.Rows.Add(s);
            }
        }
    }
}
