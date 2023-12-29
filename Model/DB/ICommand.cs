using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratory.Model.DB
{
    interface ICommand
    {
        /// <summary>
        /// Метод предназначенный для инициализации таблицы
        /// </summary>
        /// <param name="dt">таблица в которую заполняем данные из БД</param>
        /// <param name="sqlCommand">SQL запрос для получения данных из БД</param>
        SqliteDataReader InitialTable(string sqlCommand);

        /// <summary>
        /// Метод дл выполнения любого SQL запроса
        /// </summary>
        /// <param name="sqlComand"></param>
        void Execute(string sqlComand);

        /// <summary>
        /// Открывает поключение к БД
        /// </summary>
        void Open();

        /// <summary>
        /// Закрывает подключение к БД
        /// </summary>
        void Close();
    }
}