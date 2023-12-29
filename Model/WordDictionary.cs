using Laboratory.Model.DB;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Laboratory.Model
{
    public class WordDictionary
    {
        private ObservableCollection<Word> words = new ObservableCollection<Word>();
        private ChangeDB _change;

        public WordDictionary()
        {
            _change = new ChangeDB();
            words = _change.GetWords();
        }

        public void Add(Word word)
        {
            if (word != null)
            {
                word.ID = words.Count == 0 ? 1 : (words.Last().ID + 1);
                words.Add(word);
                _change.Add(word);
            }
        }

        public IEnumerable<Word> FindByRoot(string root)
        {
            return words.Where(w => root.Contains(w.Root, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public void ImportJSON()
        {
            Console.WriteLine("Введите путь к файлу");
            string path = Console.ReadLine();

            List<Word> temp = JsonConvert.DeserializeObject<List<Word>>(File.ReadAllText(path));
            int count = 0;

            temp.ForEach(x =>
            {
                if (ContainsWord(x.Full) == false)
                {                
                    x.ID = words.Count() == 0 ? 1 : (words.Last().ID + 1);
                    words.Add(x);
                    _change.Add(x);
                    count++;
                }
            });


            Console.WriteLine($"Добавленно {count} записей");
        }

        public void ImportXML()
        {
            Console.WriteLine("Введите путь к файлу");
            string path = Console.ReadLine();

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Word>));

            List<Word> temp = new List<Word>();

            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                temp = xmlSerializer.Deserialize(fs) as List<Word>;
            }

            int count = 0;

            if(temp != null)
                temp.ForEach(x =>
                {
                    if (ContainsWord(x.Full) == false)
                    {
                        x.ID = words.Count() == 0 ? 1 : (words.Last().ID + 1);
                        words.Add(x);
                        _change.Add(x);
                        count++;
                    }
                });


            Console.WriteLine($"Добавленно {count} записей");
        }

        public void ExportJson()
        {
            string json = JsonConvert.SerializeObject(words);
            File.WriteAllText(@$"JSON\{GetNameDoc()}.json", json);
        }

        public void ExportXML() 
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Word>));
            using (var writer = new StreamWriter(@$"XML\{GetNameDoc()}.xml"))
            {
                xmlSerializer.Serialize(writer, words.ToList());
            }
        }

        private string GetNameDoc()
        {
            DateTime _date = DateTime.Now;
            return $"{_date.Day} {_date.Month} {_date.Year} {_date.Hour} {_date.Minute} {_date.Second}";
        }

        public bool ContainsWord(string word)
        {
            List<Word> temp = words.ToList().Where(x => x.Full.ToLower() == word).ToList();

            if(temp.Count() == 0)
                return false;
            else return true;
        }
    }
}