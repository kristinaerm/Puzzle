using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzle
{
    class ConnDatabase
    {
        string conn_param = "Server=localhost;Port=5432;User Id=postgres;Password=postgres;Database=mybase;"; //Например: "Server=127.0.0.1;Port=5432;User Id=postgres;Password=mypass;Database=mybase;"
        NpgsqlConnection conn;
        NpgsqlCommand comm;
        public void Connection()
        {
           conn = new NpgsqlConnection(conn_param);
           comm = new NpgsqlCommand(sql, conn);
            conn.Open(); //Открываем соединение.
            
        }
        public void createTables()
        {
            string sql = "create table users (" +
           "login character(15)," +
           "pass(20) NOT NULL," +
           "PRIMARY KEY(login));";
            NpgsqlDataReader reader;
            reader = comm.ExecuteReader();
            //  result = comm.ExecuteScalar().ToString(); //Выполняем нашу команду.
            conn.Close(); //Закрываем соединение.
        }
    }
}
