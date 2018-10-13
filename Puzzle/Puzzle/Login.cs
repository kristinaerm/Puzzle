﻿using System;
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
        public Login()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ConnDatabase bd = new ConnDatabase();
            if((textBox1.Text == "admin") && (textBox2.Text == "admin"))
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
                user= bd.SelectLoginUser(login, pass);
                user[0] = user[0].Replace(" ", "");
                user[1] = user[1].Replace(" ", "");
                if ((login==user[0])&&(pass==user[1]))
                {
                    this.Hide();
                    UserFindGame usergame = new UserFindGame();
                    usergame.Show();
                }
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            ConnDatabase bd = new ConnDatabase();
            bd.InsertInUsers(textBox1.Text, textBox2.Text,"");
        }
    }
}
