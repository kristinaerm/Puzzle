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
    public partial class Profiles : Form
    {
        ConnDatabase bd = new ConnDatabase();
        public Profiles()
        {
            InitializeComponent();
            List<string[]> res = bd.SelectProfilesOfGame();
            foreach (string[] s in res)
                dataGridView1.Rows.Add(s);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            List<string[]> res = bd.SelectProfilesOfGame();
            int n = dataGridView1.CurrentRow.Index;
            dataGridView1.Rows.RemoveAt(n);
            string login = dataGridView1.Rows[n].Cells[0].Value.ToString();
            bd.DeleteGame(login);
            bd.DeleteUsers(login);
            bd.DeletePreservation(login);
        }
    }
}
