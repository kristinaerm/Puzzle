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
        public Gallery()
        {
            InitializeComponent();
        }

        private void Gallery_Load(object sender, EventArgs e)
        {
            
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            string path = openFileDialog1.FileName;//путь к файлу
            string[] expansion = path.Split('.');

            if (!expansion[2].Equals("png")) {

                MessageBox.Show("Неверный формат файла!");
                
            }
            else
            {
                textBox1.Text = openFileDialog1.FileName;
                string selectedState = comboBox1.SelectedItem.ToString();//выбор из combobox
                string name_picture= Path.GetFileNameWithoutExtension(openFileDialog1.FileName);
                ConnDatabase bd = new ConnDatabase();
                bd.InsertInGallery(path, selectedState, name_picture);//добавление в таблицу галереи
            }
        }
        

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
