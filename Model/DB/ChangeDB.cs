using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Laboratory.Model.DB
{
    public class ChangeDB
    {
        private Command _command;
        public ChangeDB()
        {
            _command = new Command();
        }
        public ObservableCollection<Word> GetWords()
        {
            List<Word> temp = new List<Word>();
            SqliteDataReader _dataTemp = _command.InitialTable("Select * from Word");

            if(_dataTemp.HasRows)
            {
                while(_dataTemp.Read())
                {
                    temp.Add(new Word()
                    {
                        ID = int.Parse(_dataTemp.GetValue(0).ToString()),
                        Full = _dataTemp.GetValue(1).ToString(),
                        Prefix = _dataTemp.GetValue(2).ToString(),
                        Root = _dataTemp.GetValue(3).ToString(),
                        Suffix = _dataTemp.GetValue(4).ToString()                      
                    });
                }
            }

            return  new ObservableCollection<Word>(temp);
        }
        public void Add(Word word)
        {
            _command.Execute($"Insert into Word (ID, Full, Root, Prefix, Suffix) values ('{word.ID}','{word.Full}', '{word.Root}', '{word.Prefix}', '{word.Suffix}')");
        }
    }
}