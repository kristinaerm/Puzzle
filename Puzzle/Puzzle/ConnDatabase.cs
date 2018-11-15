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
        //Кристина
        //string connection_parameters = "Server=localhost;Port=5432;User Id=postgres;Password=1;Database=postgres;";

        //Полина
        string connection_parameters = "Server=localhost;Port=5433;User Id=postgres;Password=0;Database=postgres;";

        NpgsqlConnection connection;

        public string cutExcessSpace(string need_to_be_cut)
        {
            string cut = "";
            int i = 0;
            while ((i < need_to_be_cut.Length) && (need_to_be_cut[i] != ' '))
            {
                cut += need_to_be_cut[i];
                i++;
            }
            return cut;
        }

        private void executeCreate(string command_sql)
        {
            connection = new NpgsqlConnection(connection_parameters);
            connection.Open();
            NpgsqlCommand command = connection.CreateCommand();
            command.CommandText = command_sql;
            command.ExecuteNonQuery();
        }





        //USERS
        public void createTableUsers()
        {
            string create_table_users = "create table users (" +
           "login character(100) NOT NULL," +
           "pass character(100) NOT NULL," +
           "summ_ballov character(100)," +
           "summ_time character(100)," +
           "PRIMARY KEY(login));";
            executeCreate(create_table_users);
        }

        public bool insertInUsers(string login, string pass, string summ_ballov, string summ_time)
        {
            try
            {
                connection = new NpgsqlConnection(connection_parameters);
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand(
                "INSERT INTO users (login,pass,summ_ballov,summ_time) VALUES(@login, @pass,@summ_ballov,@summ_time)", connection))
                {
                    command.Parameters.Add(new NpgsqlParameter("login", login));
                    command.Parameters.Add(new NpgsqlParameter("pass", pass));
                    command.Parameters.Add(new NpgsqlParameter("summ_ballov", summ_ballov));
                    command.Parameters.Add(new NpgsqlParameter("summ_time", summ_time));
                    command.ExecuteNonQuery();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<string> selectLoginAndPasswordFromUser(string login, string pass)
        {
            List<string> user = new List<string>();
            try
            {
                connection = new NpgsqlConnection(connection_parameters);
                string select_login_and_pass = "select login, pass " + "from users " +
               "where login='" + login + "'" + "and pass='" + pass + "'" + ";";
                NpgsqlCommand command = new NpgsqlCommand(select_login_and_pass, connection);
                connection.Open();
                NpgsqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    user.Add(reader.GetString(0));
                    user.Add(reader.GetString(1));
                }
                connection.Close();
            }
            catch { }
            return user;
        }

        public List<string[]> selectResultOfUsersByGamemode(string game_mode)
        {
            List<string[]> login = new List<string[]>();
            try
            {
                connection = new NpgsqlConnection(connection_parameters);
                string select = "";
                if (game_mode.Equals("На время"))
                {
                    select = "select login, summ_time from users ORDER BY summ_time DESC LIMIT 10;";
                }
                else
                {
                    select = "select login, summ_ballov from users ORDER BY summ_ballov DESC LIMIT 10;";
                }
                NpgsqlCommand command = new NpgsqlCommand(select, connection);
                connection.Open();
                NpgsqlDataReader reader = command.ExecuteReader();
                int n = reader.FieldCount;
                while (reader.Read())
                {
                    login.Add(new string[] { reader.GetString(0), reader.GetString(1) });
                }
                connection.Close();
            }
            catch { }
            return login;
        }

        public List<string[]> selectAllUsersAndResults()
        {
            List<string[]> user = new List<string[]>();
            try
            {
                connection = new NpgsqlConnection(connection_parameters);
                string select = "select login,summ_ballov,summ_time from users";
                NpgsqlCommand command = new NpgsqlCommand(select, connection);
                connection.Open();
                NpgsqlDataReader reader = command.ExecuteReader();
                int n = reader.FieldCount;
                while (reader.Read())
                {
                    user.Add(new string[] { reader.GetString(0), reader.GetString(1), reader.GetString(2) });
                }
                connection.Close();
            }
            catch { }
            return user;
        }

        public void deleteUsers(string login)
        {
            try
            {
                connection = new NpgsqlConnection(connection_parameters);
                string select = "delete from users where login='" + login + "'";
                NpgsqlCommand command = new NpgsqlCommand(select, connection);
                connection.Open();
                command.ExecuteNonQuery();
                MessageBox.Show("Учетная запись удалена!");
            }
            catch
            {
                MessageBox.Show("Учетная запись НЕ удалена!");
            }

        }





        //PUZZLE
        public void createTablePuzzle()
        {
            string create_table_puzzle = "create table puzzle (" +
           "id_puzzle character(100) NOT NULL," +
           "level_slognos character(100) NOT NULL," +
           "form_pazzla character(100) NOT NULL," +
           "id_picture character(100) NOT NULL," +
           "height character(100) NOT NULL," +
           "width character(100) NOT NULL," +
           "FOREIGN KEY (id_picture) REFERENCES gallery(id_picture)," +
           "PRIMARY KEY(id_puzzle));";
            executeCreate(create_table_puzzle);
        }

        public string insertInPuzzle(string level_slognos, string form_pazzle, string id_picture, string height, string widht)
        {
            string id_puzzle = "";
            try
            {
                connection = new NpgsqlConnection(connection_parameters);
                connection.Open();
                id_puzzle = id_picture + Guid.NewGuid().ToString();
                using (NpgsqlCommand command = new NpgsqlCommand(
                "INSERT INTO puzzle (id_puzzle,level_slognos,form_pazzla,id_picture,height,width) VALUES(@id_puzzle,@level_slognos, @form_pazzla, @id_picture,@height,@width)", connection))
                {
                    command.Parameters.Add(new NpgsqlParameter("id_puzzle", id_puzzle));
                    command.Parameters.Add(new NpgsqlParameter("level_slognos", level_slognos));
                    command.Parameters.Add(new NpgsqlParameter("form_pazzla", form_pazzle));
                    command.Parameters.Add(new NpgsqlParameter("id_picture", id_picture));
                    command.Parameters.Add(new NpgsqlParameter("height", height));
                    command.Parameters.Add(new NpgsqlParameter("width", widht));
                    command.ExecuteNonQuery();
                }
            }
            catch
            { }
            return id_puzzle;
        }

        public string selectIdPictureByIdPuzzle(string id_puzzle)
        {
            string id_picture = "";
            try
            {
                connection = new NpgsqlConnection(connection_parameters);
                string select = "select id_picture from puzzle where id_puzzle = '" + id_puzzle + "'";
                NpgsqlCommand command = new NpgsqlCommand(select, connection);
                connection.Open();
                NpgsqlDataReader reader = command.ExecuteReader();
                reader.Read();
                id_picture = reader.GetString(0);
                connection.Close();
            }
            catch { }
            return id_picture;
        }

        public List<string> selectSizeAndComplexityFromPuzzleByIdPuzzle(string id_puzzle)
        {
            List<string> picture = new List<string>();
            try
            {
                connection = new NpgsqlConnection(connection_parameters);
                string select = "select width, height,level_slognos from puzzle " +
                "where id_puzzle='" + id_puzzle + "';";
                NpgsqlCommand command = new NpgsqlCommand(select, connection);
                connection.Open();
                NpgsqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    picture.Add(reader.GetString(0));
                    picture.Add(reader.GetString(1));
                    picture.Add(reader.GetString(2));
                }
                connection.Close();
            }
            catch { }
            return picture;
        }

        public List<string[]> selectPuzzlesByComplexity(string level_slognos)
        {
            List<string[]> puzzle = new List<string[]>();
            try
            {
                connection = new NpgsqlConnection(connection_parameters);
                string select = "";
                if (level_slognos.Equals(""))
                {
                    select = "select id_puzzle, level_slognos, form_pazzla, id_picture, height, width  from puzzle order by level_slognos";
                }
                else
                {
                    select = "select id_puzzle, level_slognos, form_pazzla, id_picture, height, width from puzzle where level_slognos = '" + level_slognos + "'";
                }
                NpgsqlCommand command = new NpgsqlCommand(select, connection);
                connection.Open();
                NpgsqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    puzzle.Add(new string[] { reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5) });
                }
                connection.Close();
            }
            catch { }
            return puzzle;
        }

        public void deletePuzzle(string id)
        {
            try
            {
                connection = new NpgsqlConnection(connection_parameters);
                string select = "delete from puzzle where id_puzzle='" + id + "'";
                NpgsqlCommand command = new NpgsqlCommand(select, connection);
                connection.Open();
                command.ExecuteNonQuery();
                MessageBox.Show("Пазл удален!");
            }
            catch
            {
                MessageBox.Show("Пазл НЕ удален!");
            }

        }





        //SAVE
        public void createTableSave()
        {
            string create_table_save = "create table save (" +
           "id_puzzle character(100) NOT NULL," +
           "id_piece character(100) NOT NULL," +
           "login character(100) NOT NULL," +
           "coordinate_x character(100) NOT NULL," +
           "coordinate_y character(100) NOT NULL," +
           "FOREIGN KEY (id_puzzle) REFERENCES puzzle (id_puzzle)," +
           "FOREIGN KEY (id_piece) REFERENCES puzzle_piece(id_piece)," +
           "FOREIGN KEY (login) REFERENCES users(login)," +
           "constraint pkk primary key (id_puzzle, login, id_piece));";
            executeCreate(create_table_save);
        }

        public bool insertInSave(string id_piece, string id_game, string login, string coordinate_x, string coordinate_y)
        {
            try
            {
                connection = new NpgsqlConnection(connection_parameters);
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand(
                "INSERT INTO save (id_piece,id_game,login, coordinate_x,coordinate_y,current_result) VALUES(@id_piece,@id_game, @login, @coordinate_x,@coordinate_y)", connection))
                {
                    command.Parameters.Add(new NpgsqlParameter("id_piece", id_piece));
                    command.Parameters.Add(new NpgsqlParameter("id_game", id_game));
                    command.Parameters.Add(new NpgsqlParameter("login", login));
                    command.Parameters.Add(new NpgsqlParameter("coordinate_x", coordinate_x));
                    command.Parameters.Add(new NpgsqlParameter("coordinate_y", coordinate_y));
                    command.ExecuteNonQuery();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        //тут надо написать селект сейвов, чтобы выводить их в список доступных игр
        //в параметры передавать уровень сложности и логин

        public void deleteSave(string login)
        {
            try
            {
                connection = new NpgsqlConnection(connection_parameters);
                string select = "delete from save where login='" + login + "'";
                NpgsqlCommand command = new NpgsqlCommand(select, connection);
                connection.Open();
                command.ExecuteNonQuery();
                MessageBox.Show("Сохранения пользователя удалены!");
            }
            catch
            {
                MessageBox.Show("Сохранения пользователя НЕ удалены!");
            }

        }





        //GALLERY
        public void createTableGallery()
        {
            string create_table_gallery = "create table gallery (" +
           "id_picture character(100) NOT NULL," +
           "path_to_file character(100) NOT NULL," +
           "level_slognosty_gallery character(100) NOT NULL," +
           "name_pictures character(100) NOT NULL," +
           "PRIMARY KEY(id_picture));";
            executeCreate(create_table_gallery);
        }

        public bool insertInGallery(string path_to_file, string level_slognosty_gallery, string name_picture)
        {
            try
            {
                connection = new NpgsqlConnection(connection_parameters);
                connection.Open();
                string id_picture = Guid.NewGuid().ToString();
                using (NpgsqlCommand command = new NpgsqlCommand(
                    "INSERT INTO gallery (id_picture,path_to_file,level_slognosty_gallery,name_pictures) VALUES ('" + id_picture + "','" + path_to_file + "','" + level_slognosty_gallery + "','" + name_picture + "');", connection))
                {
                    command.ExecuteNonQuery();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<string[]> selectPathToPicturesByComplexityOrder(string complexity)
        {
            List<string[]> list = new List<string[]>();
            try
            {
                connection = new NpgsqlConnection(connection_parameters);
                string select = "";
                if (complexity.Equals("Любой"))
                {
                    select = "select path_to_file, level_slognosty_gallery " + "from gallery " +
                    "order by level_slognosty_gallery";
                }
                else
                {
                    select = "select path_to_file, level_slognosty_gallery " + "from gallery " +
                    "where level_slognosty_gallery ='" + cutExcessSpace(complexity) + "'";
                }
                NpgsqlCommand command = new NpgsqlCommand(select, connection);
                connection.Open();
                NpgsqlDataReader reader = command.ExecuteReader();

                string[] res;
                while (reader.Read())
                {
                    res = new string[reader.FieldCount];
                    for (int i = 0; i < res.Length; i++)
                    {
                        res[i] = reader.GetString(i);
                    }
                    list.Add(res);
                }
                connection.Close();
            }
            catch
            { }
            return list;
        }

        public string selectPathByIdPicture(string id)
        {
            string path = "";
            try
            {
                connection = new NpgsqlConnection(connection_parameters);
                string select = "select path_to_file from gallery where id_picture = '" + id + "'";
                NpgsqlCommand command = new NpgsqlCommand(select, connection);
                connection.Open();
                NpgsqlDataReader reader = command.ExecuteReader();
                reader.Read();
                path = reader.GetString(0);
                connection.Close();
            }
            catch { }
            return path;
        }

        public string selectIdByPathPicture(string path)
        {
            string id = "";
            try
            {
                connection = new NpgsqlConnection(connection_parameters);
                string select = "select id_picture from gallery where path_to_file = '" + path + "'";
                NpgsqlCommand command = new NpgsqlCommand(select, connection);
                connection.Open();
                NpgsqlDataReader reader = command.ExecuteReader();
                int n = reader.FieldCount;
                reader.Read();
                id = reader.GetString(0);
                connection.Close();
            }
            catch { }
            return id;
        }

        public void deletePictures(string path_to_file)
        {
            try
            {
                connection = new NpgsqlConnection(connection_parameters);
                string id_pic = cutExcessSpace(selectIdByPathPicture(path_to_file));
                string select = "delete from puzzle where id_picture='" + id_pic + "'";
                string select1 = "delete from gallery where path_to_file='" + path_to_file + "'";
                NpgsqlCommand command = new NpgsqlCommand(select, connection);
                NpgsqlCommand command1 = new NpgsqlCommand(select1, connection);
                connection.Open();
                command.ExecuteNonQuery();
                MessageBox.Show("Игры с этой картинкой удалены!");
                command1.ExecuteNonQuery();
                MessageBox.Show("Картинка удалена!");
            }
            catch
            {
                MessageBox.Show("Картинка НЕ удалена!");
            }
        }





        //PUZZLE PIECE
        public void createTablePuzzlePiece()
        {
            string create_table_puzzle_piece = "create table puzzle_piece (" +
            "id_piece character(100) NOT NULL," +
            "num_piece character(100) NOT NULL," +
            "correct_X character(100) NOT NULL," +
            "correct_Y character(100) NOT NULL," +
            "id_puzzle character(100) NOT NULL," +
            "FOREIGN KEY (id_puzzle) REFERENCES puzzle(id_puzzle)," +
            "PRIMARY KEY(id_piece));";
            executeCreate(create_table_puzzle_piece);
        }

        public string insertInPuzzlePiece(string num_piece, string correct_X, string correct_Y, string id_game)
        {
            string id_piece = "";
            try
            {
                id_piece = id_game + Guid.NewGuid().ToString();
                connection = new NpgsqlConnection(connection_parameters);
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand(
                "INSERT INTO puzzle_piece (id_piece,num_piece,correct_X,correct_Y,id_game) VALUES(@id_piece,@num_piece,@correct_X,@correct_Y, @id_game)", connection))
                {
                    command.Parameters.Add(new NpgsqlParameter("id_piece", id_piece));
                    command.Parameters.Add(new NpgsqlParameter("num_piece", id_piece));
                    command.Parameters.Add(new NpgsqlParameter("correct_X", correct_X));
                    command.Parameters.Add(new NpgsqlParameter("correct_Y", correct_Y));
                    command.Parameters.Add(new NpgsqlParameter("id_game", id_game));
                    command.ExecuteNonQuery();
                }
                return id_piece;
            }
            catch
            {
                return "";
            }

        }
        
        public string selectIdPiece(string id_game)
        {
            string id_piece = "";
            try
            {
                connection = new NpgsqlConnection(connection_parameters);
                string select = "select id_piece" + "from puzzle_piece" +
               "where id_game='" + id_game + "';";
                NpgsqlCommand command = new NpgsqlCommand(select, connection);
                connection.Open();
                NpgsqlDataReader reader = command.ExecuteReader();
                id_piece = reader.GetString(0);
                connection.Close();
            }
            catch { }
            return id_piece;
        }

        //не хватает удаления кусочка для удаления сохранений, когда игра окончена после повтороного запуска





        //GAME
        public void createTableGame()
        {
            string create_table_game = "create table game (" +
           "id_puzzle character(100) NOT NULL," +
           "login character(100) NOT NULL," +
           "build character(100) NOT NULL," +
           "game_mode character(100) NOT NULL," +
           "result character(100) NOT NULL," +
           "FOREIGN KEY (id_puzzle) REFERENCES puzzle(id_puzzle)," +
           "FOREIGN KEY (login) REFERENCES users(login)," +
           "constraint pk primary key (id_puzzle, login));";
            executeCreate(create_table_game);
        }

        public bool insertInGame(string id_puzzle, string login, string build, string game_mode, string result)
        {
            try
            {
                connection = new NpgsqlConnection(connection_parameters);
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand(
                "INSERT INTO games (id_puzzle,login,build,game_mode,result) VALUES(@id_puzzle,@login, @build, @game_mode,@result)", connection))
                {
                    command.Parameters.Add(new NpgsqlParameter("id_puzzle", id_puzzle));
                    command.Parameters.Add(new NpgsqlParameter("login", login));
                    command.Parameters.Add(new NpgsqlParameter("build", build));
                    command.Parameters.Add(new NpgsqlParameter("game_mode", game_mode));
                    command.Parameters.Add(new NpgsqlParameter("result", result));
                    command.ExecuteNonQuery();
                }
                return true;
            }
            catch
            {
                return false;
            }

        }

        public void deleteGame(string login)
        {
            try
            {
                connection = new NpgsqlConnection(connection_parameters);
                string select = "delete from game where login='" + login + "'";
                NpgsqlCommand command = new NpgsqlCommand(select, connection);
                connection.Open();
                command.ExecuteNonQuery();
                MessageBox.Show("Игры пользователя удалены!");
            }
            catch
            {
                MessageBox.Show("Игры пользователя НЕ удалены!");
            }
        }
    }
}
