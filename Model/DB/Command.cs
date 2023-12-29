using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratory.Model.DB
{
    public class Command : ICommand
    {
        //Строка подключения к БД
        private string SQLConnection = @"Data Source=Dictionary.db";
        private SqliteConnection connection;

        public Command()
        {
            connection = new SqliteConnection(SQLConnection);
        }

        //Метод для заполнения данных
        public SqliteDataReader InitialTable(string sqlCommand)
        {
            Open();
            SqliteCommand command = new SqliteCommand(sqlCommand, connection);
            SqliteDataReader dataAdapter = command.ExecuteReader();
            return dataAdapter;
        }

        //открываем подключение к БД
        public void Open()
        {
            connection.Open();
        }

        //Закрывем подключение
        public void Close()
        {
            connection.Close();
        }

        /// <summary>
        /// Метод для выполнения sql запроса к БД
        /// </summary>
        /// <param name="sqlComand"></param>
        public void Execute(string sqlComand)
        {
            connection.Open();
            SqliteCommand command = new SqliteCommand(sqlComand, connection);
            command.ExecuteNonQuery();
            connection.Close();
        }
    }
}