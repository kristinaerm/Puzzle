using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Puzzle
{
    public partial class Gallery : Form
    {
        private bool fromCreatePuzzle = false;
        private CreateGame parent = null;
        ConnDatabase bd = new ConnDatabase();

        public Gallery()
        {
            InitializeComponent();
            updateListView();
        }

        public Gallery(bool fromGame, CreateGame par)
        {
            InitializeComponent();
            parent = par;
            fromCreatePuzzle = fromGame;
            updateListView();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "png files (*.png)|*.png";
            openFileDialog1.ShowDialog();
            string path = openFileDialog1.FileName;//путь к файлу
            string FileName = System.IO.Path.GetFileName(path);
            string ext = System.IO.Path.GetExtension(FileName);
            if (!ext.Equals(".png"))
            {
                MessageBox.Show("Неверный формат файла!");
            }
            else
            {
                textBox1.Text = openFileDialog1.FileName;
                listView1.Clear();
                updateListView();
                comboBox1.Enabled = true;
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {

            string path = openFileDialog1.FileName;//путь к файлу
            string selectedState = comboBox1.SelectedItem.ToString();//выбор из combobox
            string name_picture = Path.GetFileNameWithoutExtension(openFileDialog1.FileName);
            ConnDatabase bd = new ConnDatabase();
            if (bd.insertInGallery(path, selectedState, name_picture))
            {
                listView1.Clear();
                updateListView();
                comboBox1.Enabled = false;
                button1.Enabled = false;
            }
            else MessageBox.Show("Не удалось добавить картинку");
        }
        public void updateListView()
        {
            ConnDatabase bd = new ConnDatabase();
            List<string[]> path = bd.selectPathToPicturesByComplexityOrder(comboBox2.SelectedItem.ToString());
            string s = "";
            // заполняем список изображениями
            listView1.Clear();
            foreach (string[] file in path)
            {
                // установка названия файла
                s = bd.cutExcessSpace(file[1]) +", ";
                s += file[0].Remove(0, file[0].LastIndexOf('\\') + 1);
                listView1.Items.Add(s);
            }

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<string[]> path = bd.selectPathToPicturesByComplexityOrder(comboBox2.SelectedItem.ToString());
            if (listView1.SelectedIndices.Count != 0)
            {
                if (fromCreatePuzzle == true)
                {
                    button4.Visible = true;
                    button3.Visible = true;
                }
                int t = listView1.SelectedIndices[0];
                if (t < path.Count)
                {
                    pictureBox1.Image = new Bitmap(path[t][0]);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            List<string[]> path = bd.selectPathToPicturesByComplexityOrder(comboBox2.SelectedItem.ToString());
            List<string> id_piece = new List<string>();
            if (listView1.SelectedIndices.Count != 0)
            {
                int t = listView1.SelectedIndices[0];
                string id_picture = bd.selectIdByPathPicture(path[t][0]);
                string id_puzzle = bd.selectPuzzleByIdPuzzleByIdPicture(id_picture);
                 id_piece = bd.selectIdPiece(id_puzzle);
                //удалить сначала сейвы с этой картинкой
               
                bd.deleteSaveByIdPuzzle(id_puzzle);
                bd.deleteGameByIdPuzzle(id_puzzle);
                //bd.deletePuzzle(id_puzzle);
                for(int i=0;i< id_piece.Count;i++)
                bd.deletePiecePuzzleByIdPuzzleAndOrIdPuzzle(id_puzzle, id_piece[i]);

                // bd.deletePiecePuzzleByIdPuzzleAndOrIdPicture("", bd.selectIdByPathPicture(path[t][0]));
                //удалить геймы с этой картинкой
                bd.deletePictures(path[t][0]);
                updateListView();
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            List<string[]> path = bd.selectPathToPicturesByComplexityOrder(comboBox2.SelectedItem.ToString());
            int t = 0;
            if (listView1.SelectedIndices.Count != 0)
            {
                t = listView1.SelectedIndices[0];
            }
            parent.setSelectedPic(path[t][0]);
            this.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!comboBox1.SelectedItem.ToString().Equals(""))
            {
                button1.Enabled = true;
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            updateListView();
        }
    }
}
