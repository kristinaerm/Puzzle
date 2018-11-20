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
    public partial class Login : Form
    {
        ConnDatabase bd = new ConnDatabase();
        

        public Login()
        {
            InitializeComponent();
            try
            {
                ConnDatabase bd = new ConnDatabase();
                try
                {
                    bd.createTableUsers();
                }
                catch { }
                try
                {
                    bd.createTableGallery();
                }
                catch { }
                try
                {
                    bd.createTablePuzzle();
                }
                catch { }
                try
                {
                    bd.createTableGame();
                }
                catch { }
                try
                {
                    bd.createTablePuzzlePiece();
                }
                catch { }
                try
                {
                    bd.createTableSave();
                }
                catch { }
            }
            catch { }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //войти
            if (!label3.Visible)
            {
                if ((textBox1.Text == "admin") && (textBox2.Text == "admin"))
                {
                    this.Hide();
                    CreateGame creategame = new CreateGame();
                    creategame.Show();
                }
                else
                {
                    string login = textBox1.Text;
                    string pass = textBox2.Text;
                    List<string> user = new List<string>();
                    user = bd.selectLoginAndPasswordFromUser(login, pass);
                    if (user.Count != 0)
                    {
                        this.Hide();
                        UserFindGame usergame = new UserFindGame(login);
                        usergame.Show();
                    }
                    else if (!textBox3.Visible)
                    {
                        MessageBox.Show("Нет такой комбинации логина и пароля в базе данных, проверьте правильность введенных данных или зарегистрируйте нового пользователя.");
                    }
                }
            }
            //<Вход
            else
            {
                label3.Visible = false;
                textBox3.Visible = false;
                button1.Text = "Войти";
                button2.Text = "Регистрация >";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //регистрация
            if (!label3.Visible)
            {
                label3.Visible = true;
                textBox3.Visible = true;
                button1.Text = "< Вход";
                button2.Text = "Зарегистрировать";
            }
            else //зарегистрировать
            {
                if (textBox2.Text.Equals(textBox3.Text))
                {
                    ConnDatabase bd = new ConnDatabase();
                    if (bd.insertInUsers(textBox1.Text, textBox2.Text, ""))
                    {
                        MessageBox.Show("Новая учетная запись успешно зарегистрирована! Для входа нажмите на кнопку <Вход и войдите под новой учетной записью.");
                        textBox1.Text = "";
                        textBox2.Text = "";
                        textBox3.Text = "";
                    }
                    else
                    {
                        MessageBox.Show("Такой логин уже существует!");
                    }
                }
                else
                {
                    MessageBox.Show("Пароли не совпадают");
                }
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            button2.Enabled = true;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
