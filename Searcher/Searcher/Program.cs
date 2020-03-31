using System;

namespace Searcher
{
    class Program
    {
        // Имя файла для поиска (если поиск шаблонный - null)
        public static string file_name = null;
        // Шаблон для поиска (если поиск по имени - null)
        public static string template = null;
        // Тип поиска (file / template)
        public static string mode;
        // Диск для поиска, по умолчанию C:\
        public static string start_disk = @"C:\";
        static void Main(string[] args)
        {
            // В региона проверка и получение информации с момента запуска (массив args)
            #region Checker for start
            if (args.Length == 0)
            {
                Console.WriteLine("Use: searcher.exe [VALUE] -m [FILE/TEMPLATE] -d [START POSITION]");
                Console.ReadLine();
                return;
            }

            if (args[1] == "-m")
            {
                if (args[2] == "file")
                {
                    if (string.IsNullOrWhiteSpace(args[4]))
                    {
                        Console.WriteLine("Use: searcher.exe [VALUE] -m [FILE/TEMPLATE] -d [START POSITION]");
                        Console.ReadLine();
                        return;
                    }
                    else
                    {
                        file_name = args[0];
                        mode = "file";
                    }
                }
                else if (args[2] == "template")
                {
                    if (string.IsNullOrWhiteSpace(args[4]))
                    {
                        Console.WriteLine("Use: searcher.exe [VALUE] -m [FILE/TEMPLATE] -d [START POSITION]");
                        Console.ReadLine();
                        return;
                    }
                    else
                    {
                        template = args[0];
                        mode = "template";
                    }
                }
                else
                {
                    Console.WriteLine("Use: searcher.exe [VALUE] -m [FILE/TEMPLATE] -d [START POSITION]");
                    Console.ReadLine();
                    return;
                }
                if (args[3] == "-d")
                {
                    if (string.IsNullOrWhiteSpace(args[4]))
                    {
                        Console.WriteLine("Use: searcher.exe [VALUE] -m [FILE/TEMPLATE] -d [START POSITION]");
                        Console.ReadLine();
                        return;
                    }
                    else
                    {
                        start_disk = args[4];
                    }
                }
            }

            if (string.IsNullOrWhiteSpace(start_disk) || string.IsNullOrWhiteSpace(mode))
            {
                Console.WriteLine("Use: searcher.exe [VALUE] -m [FILE/TEMPLATE] -d [START POSITION]");
                Console.ReadLine();
                return;
            } 
            #endregion

            // Создает обьекта поискового шаблона
            Searcher s = new Searcher(file_name, template, mode, start_disk);
            // Начало поиска
            s.search_engine(16);

            Console.ReadLine();
        }
    }
}
