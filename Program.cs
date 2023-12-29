
using System.ComponentModel.Design;
using System;
using Laboratory.Model;

class Program
{
    private static WordDictionary dict;
    static void Main()
    {

        dict = new WordDictionary();
        Console.WriteLine("Добро пожаловать в программу словаря однокоренных слов!");
        Menu();
    }

    static public void Menu()
    {
        Console.WriteLine("Выберите действие");
        Console.WriteLine("1. Добавить слово вручную");
        Console.WriteLine("2. Импорт слов из файла JSON");
        Console.WriteLine("3. Испорт слов из файла XMl");
        Console.WriteLine("4. Экспорт данных в JSON");
        Console.WriteLine("5. Экспорт данных в XML");
        Console.WriteLine("6. Поиск однокоренных слов по корню");
        Console.WriteLine("7. Выход из программы");

        int action = - 1;

        try
        {
            action = int.Parse(Console.ReadLine());
        }
        catch
        {
            Console.WriteLine("Не корректное значение");
            Menu();
            return;
        }

        switch(action) 
        {
            case 1: AddWord(); Menu();break;
            case 2: dict.ImportJSON(); Menu();break;
            case 3: dict.ImportXML(); Menu(); break;
            case 4: dict.ExportJson(); Menu(); break;
            case 5: dict.ExportXML(); Menu(); break;
            case 6: Find(); Menu(); break;
            case 7: break;
            default: Console.WriteLine("Некорректное дейсвие");Menu(); break;
        }
    }

    public static void Find()
    {
        Console.Write("Введите корень: ");
        string root = Console.ReadLine();

        List<Word> temp = dict.FindByRoot(root).ToList();

        if(temp.Count == 0)
        {
            Console.WriteLine("Не найденно внесенных однокоренных слов");
            return;
        }    
        else
        {
            Console.WriteLine("Однокоренные слова:");
            temp.ForEach(x => Console.WriteLine(x.Full));
        }
    }

    static private void AddWord()
    {
        Console.Write("Введите слово: ");
        string inputWord = Console.ReadLine().Trim();
        AddWordToDictionary(inputWord);
    }

    static void AddWordToDictionary(string word)
    {
        if(dict.ContainsWord(word))
        {
            Console.WriteLine("Данное слово уже внесено в словарь");
            return;
        }

        string prefix = "";
        while (true)
        {
            Console.Write("Приставка: ");
            string input = Console.ReadLine().Trim();
            if (string.IsNullOrWhiteSpace(input))
            {
                break;
            }

            prefix += input;
        }

        Console.Write("Корень: ");
        string root = Console.ReadLine().Trim();

        string suffix = "";
        while (true)
        {
            Console.Write("Суффикс или окончание: ");
            string input = Console.ReadLine().Trim();

            if (string.IsNullOrWhiteSpace(input))
            {
                break;
            }

            suffix += input;
        }

        string newWord = ($"{prefix}{root}{suffix}");

        if (!IsWordValid(newWord, word))
        {
            Console.WriteLine("Неверные части слова. Пожалуйста, повторите ввод.");
            return;
        }

        Word w = new Word();
        w.Prefix = prefix;
        w.Root = root;
        w.Suffix = suffix;
        w.Full = word;

        dict.Add(w);

        Console.WriteLine($"Слово \"{newWord}\" добавлено.");
    }

    static bool IsWordValid(string word, string originalWord)
    {
        string[] parts = word.Split('-');
        string assembledWord = string.Join("", parts);

        return assembledWord == originalWord;
    }

    
        }
    
