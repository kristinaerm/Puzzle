using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Puzzle
{
    class ConnDatabase
    {
        string conn_param = "Server=localhost;Port=5432;User Id=postgres;Password=1;Database=postgres;"; 
        NpgsqlConnection conn;
        NpgsqlCommand comm;
         public void createTablesUsers()
        {
            conn = new NpgsqlConnection(conn_param);
            conn.Open(); //Открываем соединение.
            string createTableUser = "create table users (" +
           "login character(100) NOT NULL," +
           "pass character(100) NOT NULL," +
           "summ_ballov character(100)," +
           "PRIMARY KEY(login));";
            var command = conn.CreateCommand();
            command.CommandText = createTableUser;
            command.ExecuteNonQuery();
            conn.Close(); //Закрываем соединение.
        }
        public void createTablesGames()
        {
            conn = new NpgsqlConnection(conn_param);
            conn.Open(); //Открываем соединение.
            string createTableGames = "create table games (" +
           "id_game character(100)," +
           "level_slognos character(100) NOT NULL," +
           "form_pazzla character(100) NOT NULL," +
           "id_picture character(100) NOT NULL," +
           "height character(100) NOT NULL," +
           "width character(100) NOT NULL," +
           "PRIMARY KEY(id_game));";
            var command = conn.CreateCommand();
            command.CommandText = createTableGames;
            command.ExecuteNonQuery();
            conn.Close(); //Закрываем соединение.
        }
        public void createTablesSave()
        {
            conn = new NpgsqlConnection(conn_param);
            conn.Open(); //Открываем соединение.
            string createTableSave = "create table preservation (" +
           "id_game character(100)," +
           "login character(100) NOT NULL," +
           "coordinate_x character(100) NOT NULL," +
           "coordinate_y character(100) NOT NULL," +
           "constraint pk primary key (id_game, login));";
            var command = conn.CreateCommand();
            command.CommandText = createTableSave;
            command.ExecuteNonQuery();
            conn.Close(); //Закрываем соединение.
        }
        public void createTablesGallery()
        {
            conn = new NpgsqlConnection(conn_param);
            conn.Open(); //Открываем соединение.
            string createTableSave = "create table gallery (" +
           "id_picture character(100)," +
           "path_to_file character(100) NOT NULL," +
           "level_slognosty_gallery character(100) NOT NULL," +
           "name_pictures character(100) NOT NULL," +
           "PRIMARY KEY(id_picture));";
            var command = conn.CreateCommand();
            command.CommandText = createTableSave;
            command.ExecuteNonQuery();
            conn.Close(); //Закрываем соединение.
        }
        public string[] selectPictureForGallery()
        {
            conn = new NpgsqlConnection(conn_param);
            string selectpathpoctures = "select path_to_file,level_slognosty_gallery " +
           "from gallery";
            NpgsqlCommand command = new NpgsqlCommand(selectpathpoctures, conn);
            conn.Open(); //Открываем соединение.
            NpgsqlDataReader reader = command.ExecuteReader();
 
            string[] path = new string[reader.FieldCount];
            string[] level = new string[reader.FieldCount];
            string[] name = new string[reader.FieldCount];
            while (reader.Read())
            {
                for (int i = 0; i < path.Length; i++)
                {
                    path[i] = reader.GetString(1);
                    level[i] = reader.GetString(2);
                    name[i] = reader.GetString(3);
                }
            }
            //надо вернуть 3 массива(пока не знаю как)
            return path;
        }
        public void InsertInUsers(string login,string pass,string sum_ballov)
        {
            conn = new NpgsqlConnection(conn_param);
            conn.Open(); //Открываем соединение.
            using (NpgsqlCommand command = new NpgsqlCommand(
            "INSERT INTO users (login,pass,summ_ballov) VALUES(@id_user, @login, @pass,@sum_ballov)", conn)) 
            {
                try
                {
                    command.Parameters.Add(new NpgsqlParameter("login", login));
                    command.Parameters.Add(new NpgsqlParameter("pass", pass));
                    command.Parameters.Add(new NpgsqlParameter("summ_ballov", sum_ballov));
                    command.ExecuteNonQuery();
                }
                catch
                {
                    MessageBox.Show("Такой логин уже существует");
                }
            }
        
            
        }
        public void InsertInGame(string level_slognos, string form_pazzle,string id_picture, string height, string widht)
        {
            conn = new NpgsqlConnection(conn_param);
            conn.Open(); //Открываем соединение.
            string id_game = id_picture + Guid.NewGuid().ToString();//уникальный идентификатор игры
            using (NpgsqlCommand command = new NpgsqlCommand(
            
            "INSERT INTO games (id_game,level_slognos,form_pazzle,id_pictures,height,widht) VALUES(@id_game,@id_user, @login, @pass,@sum_ballov)", conn))
            {
                
                try
                {
                    command.Parameters.Add(new NpgsqlParameter("id_game", id_game));
                    command.Parameters.Add(new NpgsqlParameter("level_slognos", level_slognos));
                    command.Parameters.Add(new NpgsqlParameter("form_pazzle", form_pazzle));
                    command.Parameters.Add(new NpgsqlParameter("id_picture", id_picture));
                    command.Parameters.Add(new NpgsqlParameter("height", height));
                    command.Parameters.Add(new NpgsqlParameter("widht", widht));
                    command.ExecuteNonQuery();
                    MessageBox.Show("Игра сохранена!");
                }
                catch
                {
                    MessageBox.Show("Ошибка! Проверьте введеные данные");
                }
            }
        }
        public void InsertInGallery(string path_to_file, string level_slognosty_gallery,string name_picture)
        {
            conn = new NpgsqlConnection(conn_param);
            conn.Open(); //Открываем соединение.
            string id_picture = Guid.NewGuid().ToString();//уникальный идентификатор картинки;
            using (NpgsqlCommand command = new NpgsqlCommand(
         //   "INSERT INTO gallery (id_pictures,path_to_file,level_slognosty_gallery,name_picture) VALUES(@id_picture, @path_to_file, @level_slognosty_gallery,@name_picture)", conn))
                "INSERT INTO gallery (id_picture,path_to_file,level_slognosty_gallery,name_pictures) VALUES ('" + id_picture + "','" + path_to_file + "','" + level_slognosty_gallery + "','" + name_picture + "');", conn))
            {
                try
                {

                    command.ExecuteNonQuery();
                    MessageBox.Show("Картинка добавлена в галерею!");

                }
                catch
            {
                MessageBox.Show("Ошибка!");
            }
        }
        }
        public void InsertInSave(string id_game,string login,string coordinate_x, string coordinate_y)
        {
            conn = new NpgsqlConnection(conn_param);
            conn.Open(); //Открываем соединение.
            using (NpgsqlCommand command = new NpgsqlCommand(
            "INSERT INTO preservation (id_game,login,coordinate_x,coordinate_y) VALUES(@id_game, @login, @coordinate_x,@coordinate_y)", conn))
            {
                try
                {
                    command.Parameters.Add(new NpgsqlParameter("id_game", id_game));
                    command.Parameters.Add(new NpgsqlParameter("login", login));
                    command.Parameters.Add(new NpgsqlParameter("coordinate_x", coordinate_x));
                    command.Parameters.Add(new NpgsqlParameter("coordinate_y", coordinate_y));
                    command.ExecuteNonQuery();
                     }
                catch
                {
                    MessageBox.Show("Ошибка!");
                }
            }
        }
        public string SelectIdPicture(string path_to_file, string level_slognosty_gallery, string name_pictures)
        {
            conn = new NpgsqlConnection(conn_param);
            string selectpathpoctures = "select id_pictures" +
           "from gallery"+
           "where path_to_file="+ path_to_file + "and level_slognosty_gallery="+ level_slognosty_gallery+ "and name_pictures=" + name_pictures;
            NpgsqlCommand command = new NpgsqlCommand(selectpathpoctures, conn);
            conn.Open(); //Открываем соединение.
            NpgsqlDataReader reader = command.ExecuteReader();
            string id_pictures = reader.GetString(0);
          
            return id_pictures;
        
        }
        public string SelectIdGame(string id_pictures)
        {
            conn = new NpgsqlConnection(conn_param);
            string selectpathpoctures = "select id_game " +
           "from games" +
           "where id_pictures=" + id_pictures; 
            NpgsqlCommand command = new NpgsqlCommand(selectpathpoctures, conn);
            conn.Open(); //Открываем соединение.
            NpgsqlDataReader reader = command.ExecuteReader();
            string id_game = reader.GetString(0);
            return id_game;
        }
        public List<string> SelectPathPicture()
        {
            conn = new NpgsqlConnection(conn_param);
            string selectpathpoctures = "select path_to_file from gallery";
            NpgsqlCommand command = new NpgsqlCommand(selectpathpoctures, conn);
            conn.Open(); //Открываем соединение.
            NpgsqlDataReader reader = command.ExecuteReader();
            int n = reader.FieldCount;
            List<string> path_to_file =new List<string>();
            while (reader.Read())
            {
               
                    path_to_file.Add(reader.GetString(0));
                    
                
            }
            return path_to_file;
        }
    }
}
