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
        public Gallery()
        {
            InitializeComponent();
            ConnDatabase bd = new ConnDatabase();
            //bd.createTablesUsers();
            //bd.createTablesGallery();
            //bd.createTablesPuzzle();
            //bd.createTablesGame();
            //bd.createTablesPuzzlePiece();
            //bd.createTablesSave();
            updateListView();
        }

        private CreateGame parent = null;

        public Gallery(bool fromGame, CreateGame par)
        {
            InitializeComponent();
            parent = par;
            fromCreatePuzzle = fromGame;
            ConnDatabase bd = new ConnDatabase();
            //bd.createTablesUsers();
            //bd.createTablesGallery();
            //bd.createTablesPuzzle();
            //bd.createTablesGame();
            //bd.createTablesPuzzlePiece();
<<<<<<< HEAD

           // bd.createTablesSave();

            //bd.createTablesSave();

=======
            // bd.createTablesSave();
>>>>>>> 15d667cab3ddcfa72de181025217b23805b4f292
            updateListView();
        }

        private void Gallery_Load(object sender, EventArgs e)
        {


        }

        private void button2_Click(object sender, EventArgs e)
        {
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
            bd.InsertInGallery(path, selectedState, name_picture);//добавление в таблицу галереи
            listView1.Clear();
            updateListView();
            comboBox1.Enabled = false;
            button1.Enabled = false;

        }
        public void updateListView()
        {
            ConnDatabase bd = new ConnDatabase();
            List<string> path = bd.SelectPathPicture();
            string s = "";
            // заполняем список изображениями
            listView1.Clear();
            foreach (string file in path)
            {
                // установка названия файла
                s = file.Remove(0, file.LastIndexOf('\\') + 1);
                listView1.Items.Add(s);
            }

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

            ConnDatabase bd = new ConnDatabase();
            List<string> path = bd.SelectPathPicture();

            if (listView1.SelectedIndices.Count != 0)
            {
                if (fromCreatePuzzle == true)
                {
                    button4.Visible = true;
                    button3.Visible = true;
                }
                int t = listView1.SelectedIndices[0];
                //индекс больше коллекции
                if (t < path.Count)
                {
                    pictureBox1.Image = new Bitmap(path[t]);
                }

            }


        }

        private void button3_Click(object sender, EventArgs e)
        {
            ConnDatabase bd = new ConnDatabase();
            List<string> path = bd.SelectPathPicture();
            if (listView1.SelectedIndices.Count != 0)
            {
                int t = listView1.SelectedIndices[0];
                bd.DeletePictures(path[t]);
                updateListView();
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            ConnDatabase bd = new ConnDatabase();
            List<string> path = bd.SelectPathPicture();
            int t = 0;
            if (listView1.SelectedIndices.Count != 0)
            {
                t = listView1.SelectedIndices[0];
            }
            parent.setSelectedPic(path[t]);

            //нужно передать картинку на форму CreateGame
            CreateGame cg = new CreateGame(true);
            cg.Owner = this; //Передаём вновь созданной форме её владельца.
            cg.Show();
            Close();
        }
        //передача выбранной картинки в CreateGame
        public string DataForm()
        {
            ConnDatabase bd = new ConnDatabase();
            List<string> path = bd.SelectPathPicture();
            int t = 0;
            if (listView1.SelectedIndices.Count != 0)
            {
                t = listView1.SelectedIndices[0];

                // pictureBox1.Image = new Bitmap(path[t]);
            }

            return path[t];
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!comboBox1.SelectedItem.ToString().Equals(""))
            {
                button1.Enabled = true;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
