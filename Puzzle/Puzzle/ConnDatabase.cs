﻿using Npgsql;
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
        //Кристина
        string conn_param = "Server=localhost;Port=5432;User Id=postgres;Password=1;Database=postgres;";

        //Полина
        //string conn_param = "Server=localhost;Port=5433;User Id=postgres;Password=0;Database=postgres;";

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
           "summ_time character(100)," +
           "PRIMARY KEY(login));";
            var command = conn.CreateCommand();
            command.CommandText = createTableUser;
            command.ExecuteNonQuery();
            conn.Close(); //Закрываем соединение.
        }

        public void createTablesPuzzle()
        {
            conn = new NpgsqlConnection(conn_param);
            conn.Open(); //Открываем соединение.
            string createTableGames = "create table puzzle (" +
           "id_puzzle character(100) NOT NULL," +
           "level_slognos character(100) NOT NULL," +
           "form_pazzla character(100) NOT NULL," +
           "id_picture character(100) NOT NULL," +
           "height character(100) NOT NULL," +
           "width character(100) NOT NULL," +
           "FOREIGN KEY (id_picture) REFERENCES gallery(id_picture)," +
           "PRIMARY KEY(id_puzzle));";
            var command = conn.CreateCommand();
            command.CommandText = createTableGames;
            command.ExecuteNonQuery();
            conn.Close(); //Закрываем соединение.
        }

        //добавила current_result
        public void createTablesSave()
        {
            conn = new NpgsqlConnection(conn_param);
            conn.Open(); //Открываем соединение.
            string createTableSave = "create table save (" +
           "id_puzzle character(100) NOT NULL," +
           "id_piece character(100) NOT NULL," +
           "login character(100) NOT NULL," +
           "coordinate_x character(100) NOT NULL," +
           "coordinate_y character(100) NOT NULL," +
           "current_result character(100) NOT NULL," +
           "FOREIGN KEY (id_puzzle) REFERENCES puzzle (id_puzzle)," +
           "FOREIGN KEY (id_piece) REFERENCES puzzle_piece(id_piece)," +
           "FOREIGN KEY (login) REFERENCES users(login)," +
           "constraint pkk primary key (id_puzzle, login, id_piece));";
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
           "id_picture character(100) NOT NULL," +
           "path_to_file character(100) NOT NULL," +
           "level_slognosty_gallery character(100) NOT NULL," +
           "name_pictures character(100) NOT NULL," +
           "PRIMARY KEY(id_picture));";
            var command = conn.CreateCommand();
            command.CommandText = createTableSave;
            command.ExecuteNonQuery();
            conn.Close(); //Закрываем соединение.
        }

        public void createTablesPuzzlePiece()
        {
            conn = new NpgsqlConnection(conn_param);
            conn.Open(); //Открываем соединение.
            string createTableSave = "create table puzzle_piece (" +
           "id_piece character(100) NOT NULL," +
           "path_to_piece character(100) NOT NULL," +
           "id_puzzle character(100) NOT NULL," +
           "FOREIGN KEY (id_puzzle) REFERENCES puzzle(id_puzzle)," +
           "PRIMARY KEY(id_piece));";
            var command = conn.CreateCommand();
            command.CommandText = createTableSave;
            command.ExecuteNonQuery();
            conn.Close(); //Закрываем соединение.
        }

        public void createTablesGame()
        {
            conn = new NpgsqlConnection(conn_param);
            conn.Open(); //Открываем соединение.
            string createTableSave = "create table game (" +
           "id_puzzle character(100) NOT NULL," +
           "login character(100) NOT NULL," +
           "build character(100) NOT NULL," +
           "game_mode character(100) NOT NULL," +
           "result character(100) NOT NULL," +
           "FOREIGN KEY (id_puzzle) REFERENCES puzzle(id_puzzle)," +
           "FOREIGN KEY (login) REFERENCES users(login)," +
           "constraint pk primary key (id_puzzle, login));";
            var command = conn.CreateCommand();
            command.CommandText = createTableSave;
            command.ExecuteNonQuery();
            conn.Close(); //Закрываем соединение.
        }

        public void InsertInGame(string id_puzzle, string login, string build, string game_mode, string result)
        {
            conn = new NpgsqlConnection(conn_param);
            conn.Open(); //Открываем соединение.
            using (NpgsqlCommand command = new NpgsqlCommand(

            "INSERT INTO games (id_puzzle,login,build,game_mode,result) VALUES(@id_puzzle,@login, @build, @game_mode,@result)", conn))
            {
                command.Parameters.Add(new NpgsqlParameter("id_puzzle", id_puzzle));
                command.Parameters.Add(new NpgsqlParameter("login", login));
                command.Parameters.Add(new NpgsqlParameter("build", build));
                command.Parameters.Add(new NpgsqlParameter("game_mode", game_mode));
                command.Parameters.Add(new NpgsqlParameter("result", result));
                command.ExecuteNonQuery();
            }
        }

        public bool InsertInUsers(string login, string pass, string summ_ballov, string summ_time)
        {
            conn = new NpgsqlConnection(conn_param);
            conn.Open(); //Открываем соединение.
            using (NpgsqlCommand command = new NpgsqlCommand(
            "INSERT INTO users (login,pass,summ_ballov,summ_time) VALUES(@login, @pass,@summ_ballov,@summ_time)", conn))
            {
                try
                {
                    command.Parameters.Add(new NpgsqlParameter("login", login));
                    command.Parameters.Add(new NpgsqlParameter("pass", pass));
                    command.Parameters.Add(new NpgsqlParameter("summ_ballov", summ_ballov));
                    command.Parameters.Add(new NpgsqlParameter("summ_time", summ_time));
                    command.ExecuteNonQuery();
                    return true;
                }
                catch
                {
                    MessageBox.Show("Такой логин уже существует");
                    return false;
                }
            }


        }
        public string InsertInPuzzle(string level_slognos, string form_pazzle, string id_picture, string height, string widht)
        {
            string id_puzzle = "";
            conn = new NpgsqlConnection(conn_param);
            conn.Open(); //Открываем соединение.
            id_puzzle = id_picture + Guid.NewGuid().ToString();//уникальный id
            using (NpgsqlCommand command = new NpgsqlCommand(

            "INSERT INTO puzzle (id_puzzle,level_slognos,form_pazzla,id_picture,height,width) VALUES(@id_puzzle,@level_slognos, @form_pazzla, @id_picture,@height,@width)", conn))
            {
                //try
                //{
                int o = id_picture.Length;
                    command.Parameters.Add(new NpgsqlParameter("id_puzzle", id_puzzle));
                    command.Parameters.Add(new NpgsqlParameter("level_slognos", level_slognos));
                    command.Parameters.Add(new NpgsqlParameter("form_pazzla", form_pazzle));
                    command.Parameters.Add(new NpgsqlParameter("id_picture", id_picture));
                    command.Parameters.Add(new NpgsqlParameter("height", height));
                    command.Parameters.Add(new NpgsqlParameter("width", widht));
                    command.ExecuteNonQuery();
                    MessageBox.Show("Игра сохранена!");
                //}
                //catch
                //{
                //    MessageBox.Show("Ошибка!Такой пазл уже есть");
                //    id_puzzle = "";
                //}
            }
            return id_puzzle;
        }

        public void InsertInGallery(string path_to_file, string level_slognosty_gallery, string name_picture)
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

        //добавила current_result
        public void InsertInSave(string id_piece, string id_game, string login, string coordinate_x, string coordinate_y, string current_result)
        {
            conn = new NpgsqlConnection(conn_param);
            conn.Open(); //Открываем соединение.
            using (NpgsqlCommand command = new NpgsqlCommand(
            "INSERT INTO preservation (id_piece,id_game,login, coordinate_x,coordinate_y,current_result) VALUES(@id_piece,@id_game, @login, @coordinate_x,@coordinate_y, @current_result)", conn))
            {
                try
                {
                    command.Parameters.Add(new NpgsqlParameter("id_piece", id_piece));
                    command.Parameters.Add(new NpgsqlParameter("id_game", id_game));
                    command.Parameters.Add(new NpgsqlParameter("login", login));
                    command.Parameters.Add(new NpgsqlParameter("coordinate_x", coordinate_x));
                    command.Parameters.Add(new NpgsqlParameter("coordinate_y", coordinate_y));
                    command.Parameters.Add(new NpgsqlParameter("current_result", current_result));
                    command.ExecuteNonQuery();
                }
                catch
                {
                    MessageBox.Show("Ошибка с базой данных!");
                }
            }
        }

        public void InsertInPuzzlePiece(string path_to_piece, string id_game)
        {
            string id_piece = id_game + Guid.NewGuid().ToString();//уникальный идентификатор кусочка пазла;
            conn = new NpgsqlConnection(conn_param);
            conn.Open(); //Открываем соединение.
            using (NpgsqlCommand command = new NpgsqlCommand(
            "INSERT INTO puzzle_piece (id_piece,path_to_piece,id_game) VALUES(@id_piece,@path_to_piece, @id_game)", conn))
            {
                try
                {
                    command.Parameters.Add(new NpgsqlParameter("id_piece", id_piece));
                    command.Parameters.Add(new NpgsqlParameter("path_to_piece", path_to_piece));
                    command.Parameters.Add(new NpgsqlParameter("id_game", id_game));
                    command.ExecuteNonQuery();
                }
                catch
                {
                    MessageBox.Show("Ошибка c базой данных!");
                }
            }
        }

        public string selectPicture(string id)
        {
            conn = new NpgsqlConnection(conn_param);
            string selectpathpoctures = "select path_to_file from gallery where id_picture = '"+id+"'";
            NpgsqlCommand command = new NpgsqlCommand(selectpathpoctures, conn);
            conn.Open(); //Открываем соединение.
            NpgsqlDataReader reader = command.ExecuteReader();

            string path = "";
            reader.Read();
            path = reader.GetString(0);
            conn.Close();
            return path;
        }
        public string selectIdPicture(string id_puzzle)
        {
            conn = new NpgsqlConnection(conn_param);
            string selectpathpoctures = "select id_picture from puzzle where id_puzzle = '" + id_puzzle + "'";
            NpgsqlCommand command = new NpgsqlCommand(selectpathpoctures, conn);
            conn.Open(); //Открываем соединение.
            NpgsqlDataReader reader = command.ExecuteReader();

            string id_picture = "";
            reader.Read();
            id_picture = reader.GetString(0);
            conn.Close();
            return id_picture;
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
                    path[i] = reader.GetString(0);
                    level[i] = reader.GetString(1);
                    name[i] = reader.GetString(2);
                }
            }
            conn.Close();
            //надо вернуть 3 массива(пока не знаю как)
            return path;
        }

        public string SelectIdPiece(string id_game)
        {
            conn = new NpgsqlConnection(conn_param);
            string selectpathpoctures = "select id_piece" +
           "from puzzle_piece" +
           "where id_game='" + id_game + "';";
            NpgsqlCommand command = new NpgsqlCommand(selectpathpoctures, conn);
            conn.Open(); //Открываем соединение.
            NpgsqlDataReader reader = command.ExecuteReader();
            string id_piece = reader.GetString(0);
            conn.Close();
            return id_piece;
        }

        public List<string> SelectLoginUser(string login, string pass)
        {
            conn = new NpgsqlConnection(conn_param);
            List<string> user = new List<string>();
            string selectpathpoctures = "select login, pass " +
           "from users " +
           "where login='" + login + "'" + "and pass='" + pass + "'" + ";";
            NpgsqlCommand command = new NpgsqlCommand(selectpathpoctures, conn);
            conn.Open(); //Открываем соединение.
            NpgsqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                user.Add(reader.GetString(0));
                user.Add(reader.GetString(1));
            }
            conn.Close();
            return user;
        }
     
        public string SelectIdPicture(string path_to_file, string level_slognosty_gallery, string name_pictures)
        {
            conn = new NpgsqlConnection(conn_param);
            string selectpathpoctures = "select id_pictures" +
           "from gallery" +
           "where path_to_file='" + path_to_file + "' and level_slognosty_gallery='" + level_slognosty_gallery + "' and name_pictures='" + name_pictures + "'";
            NpgsqlCommand command = new NpgsqlCommand(selectpathpoctures, conn);
            conn.Open(); //Открываем соединение.
            NpgsqlDataReader reader = command.ExecuteReader();
            string id_pictures = null;
            while (reader.Read())
            {
                id_pictures = reader.GetString(0);
            }
            conn.Close();
            return id_pictures;
        }

        public string SelectIdPuzzle(string id_pictures)
        {
            conn = new NpgsqlConnection(conn_param);
            string selectpathpoctures = "select id_puctures " +
           "from puzzle" +
           "where id_pictures='" + id_pictures + "'";
            NpgsqlCommand command = new NpgsqlCommand(selectpathpoctures, conn);
            conn.Open(); //Открываем соединение.
            NpgsqlDataReader reader = command.ExecuteReader();
            string id_puzzle = null;
            while (reader.Read())
            {
                id_puzzle = reader.GetString(0);
            }
            conn.Close();
            return id_puzzle;
        }

        public List<string> SelectPathPicture()
        {
            conn = new NpgsqlConnection(conn_param);
            string selectpathpoctures = "select path_to_file from gallery";
            NpgsqlCommand command = new NpgsqlCommand(selectpathpoctures, conn);
            conn.Open(); //Открываем соединение.
            NpgsqlDataReader reader = command.ExecuteReader();
            int n = reader.FieldCount;
            List<string> path_to_file = new List<string>();
            while (reader.Read())
            {
                path_to_file.Add(reader.GetString(0));
            }
            conn.Close();
            return path_to_file;
        }
        public string SelectPathPicture(string id_picture)
        {
            conn = new NpgsqlConnection(conn_param);

            string selectpathpoctures = "select path_to_file from gallery where id_picture= '" + id_picture.Replace(" ","") + "';";
            NpgsqlCommand command = new NpgsqlCommand(selectpathpoctures, conn);
            conn.Open(); //Открываем соединение.
            NpgsqlDataReader reader = command.ExecuteReader();
            string path_to_file = "";
            while (reader.Read())
            {
               path_to_file = reader.GetString(0);
            }
            conn.Close();
            return path_to_file;
        }
        //возвращает ширину,высоту, уровень сложности
        public List<string> SelectInPuzzle(string id_puzzel)
        {
            List<string> picture = new List<string>();
            conn = new NpgsqlConnection(conn_param);
            string selectpathpoctures = "select width, height,level_slognos from puzzle where id_puzzle='" + id_puzzel + "';";
            NpgsqlCommand command = new NpgsqlCommand(selectpathpoctures, conn);
            conn.Open(); //Открываем соединение.
            NpgsqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {

                picture.Add( reader.GetString(0));
                picture.Add(reader.GetString(1));
                picture.Add(reader.GetString(2));
            }
            conn.Close();
            return picture;
        }

        public string SelectIdPictureByPath(string path)
        {
            conn = new NpgsqlConnection(conn_param);
            string selectpathpoctures = "select id_picture from gallery where path_to_file = '"+path+"'";
            NpgsqlCommand command = new NpgsqlCommand(selectpathpoctures, conn);
            conn.Open(); //Открываем соединение.
            NpgsqlDataReader reader = command.ExecuteReader();
            int n = reader.FieldCount;
            string id = "";
            reader.Read();
            id = reader.GetString(0);
            int o = id.Length;
            conn.Close();
            return id;
        }

        public List<string[]> SelectResultOfGame(string game_mode)
        {
            conn = new NpgsqlConnection(conn_param);
            string selectpathpoctures = "";
            if (game_mode.Equals("На время"))
            {
                 selectpathpoctures = "select login, summ_time from users ORDER BY summ_time DESC LIMIT 10;";
            }
            else
            {
                selectpathpoctures = "select login, summ_ballov from users ORDER BY summ_ballov DESC LIMIT 10;";
            }            
            NpgsqlCommand command = new NpgsqlCommand(selectpathpoctures, conn);
            conn.Open(); //Открываем соединение.
            NpgsqlDataReader reader = command.ExecuteReader();
            int n = reader.FieldCount;
            List<string[]> login = new List<string[]>();

            while (reader.Read())
            {
                login.Add(new string[] { reader.GetString(0), reader.GetString(1) });
            }
            conn.Close();
            return login;
        }

        public List<string[]> SelectProfilesOfGame()
        {
            conn = new NpgsqlConnection(conn_param);
            string selectpathpoctures = "select login,summ_ballov,summ_time from users";
            NpgsqlCommand command = new NpgsqlCommand(selectpathpoctures, conn);
            conn.Open(); //Открываем соединение.
            NpgsqlDataReader reader = command.ExecuteReader();
            int n = reader.FieldCount;
            List<string[]> user = new List<string[]>();

            while (reader.Read())
            {
                user.Add(new string[] { reader.GetString(0), reader.GetString(1), reader.GetString(2) });
            }
            conn.Close();
            return user;
        }

        public List<string[]> SelectPuzzles(string level_slognos)
        {
            conn = new NpgsqlConnection(conn_param);
            string selectpathpoctures = "";
            if (level_slognos.Equals(""))
            {
                selectpathpoctures = "select id_puzzle, level_slognos, form_pazzla, id_picture, height, width  from puzzle order by level_slognos";
            }
            else
            {
                selectpathpoctures = "select id_puzzle, level_slognos, form_pazzla, id_picture, height, width from puzzle where level_slognos = '" + level_slognos + "'";
            }
            
            NpgsqlCommand command = new NpgsqlCommand(selectpathpoctures, conn);
            conn.Open(); //Открываем соединение.
            NpgsqlDataReader reader = command.ExecuteReader();
            int n = reader.FieldCount;
            List<string[]> puzzle = new List<string[]>();

            while (reader.Read())
            {
                puzzle.Add(new string[] { reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5) });
            }
            conn.Close();
            return puzzle;
        }

        public void DeletePictures(string path_to_file)
        {
          
                conn = new NpgsqlConnection(conn_param);
                string selectpathpoctures = "delete from gallery where path_to_file='" + path_to_file + "'";
                NpgsqlCommand command = new NpgsqlCommand(selectpathpoctures, conn);
                conn.Open(); //Открываем соединение.
                command.ExecuteNonQuery();
                MessageBox.Show("Картинка удалена!");
          
        }
        

        public void DeleteUsers(string login)
        {
            conn = new NpgsqlConnection(conn_param);
            string selectpathpoctures = "delete from users where login='" + login + "'";
            NpgsqlCommand command = new NpgsqlCommand(selectpathpoctures, conn);
            conn.Open(); //Открываем соединение.
            command.ExecuteNonQuery();
            MessageBox.Show("Учетная запись удалена!");
        }

        public void DeleteGame(string login)
        {
            conn = new NpgsqlConnection(conn_param);
            string selectpathpoctures = "delete from game where login='" + login + "'";
            NpgsqlCommand command = new NpgsqlCommand(selectpathpoctures, conn);
            conn.Open(); //Открываем соединение.
            command.ExecuteNonQuery();
            MessageBox.Show("Картинка удалена!");
        }

        public void DeleteSave(string login)
        {
            conn = new NpgsqlConnection(conn_param);
            string selectpathpoctures = "delete from save where login='" + login + "'";
            NpgsqlCommand command = new NpgsqlCommand(selectpathpoctures, conn);
            conn.Open(); //Открываем соединение.
            command.ExecuteNonQuery();
            MessageBox.Show("Сохранения пользователя удалены!");
        }

      

    }
}
