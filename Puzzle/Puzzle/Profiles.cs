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
        private ConnDatabase bd = new ConnDatabase();
        public Profiles()
        {
            InitializeComponent();
            List<string[]> res = bd.selectAllUsersAndResults();
            foreach (string[] s in res)
            {
                for (int i=0; i<s.Length; i++)
                {
                    s[i] = bd.cutExcessSpace(s[i]);
                }
                dataGridView1.Rows.Add(s);
            }
                
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            Point xy = dataGridView1.CurrentCellAddress;
            int x = xy.X;
            int y = xy.Y;
            if (x == 3)
            {
                List<string[]> res = bd.selectAllUsersAndResults();
                if (y < res.Count)
                {                    
                    string login = dataGridView1.Rows[y].Cells[0].Value.ToString();
                    bd.deleteSaveByLogin(login);
                    bd.deleteGameByLogin(login);
                    bd.deleteUsers(login);
                    dataGridView1.Rows.RemoveAt(y);
                }                
            }
        }

        private void Profiles_Load(object sender, EventArgs e)
        {

        }
    }
}
