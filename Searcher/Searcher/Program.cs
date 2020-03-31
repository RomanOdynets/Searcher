using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Searcher
{
    class Program
    {
        public static string file_name = null;
        public static string template = null;
        public static string mode;
        public static string start_disk;
        static void Main(string[] args)
        {
            if(args.Length == 0)
            {
                Console.WriteLine("Use: searcher.exe [VALUE] -m [FILE/TEMPLATE] -d [START POSITION]");
                Console.ReadLine();
                return;
            }

            if(args[1] == "-m")
            {
                if(args[2] == "file")
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
                else if(args[2] == "template")
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
                if(args[3] == "-d")
                {
                    if(string.IsNullOrWhiteSpace(args[4]))
                    {
                        Console.WriteLine("Use: searcher.exe [VALUE] -m [FILE/TEMPLATE] -d [START POSITION]");
                        Console.ReadLine();
                        return;
                    } else
                    {
                        start_disk = args[4];
                    }
                }
            }

            if(string.IsNullOrWhiteSpace(start_disk) || string.IsNullOrWhiteSpace(mode))
            {
                Console.WriteLine("Use: searcher.exe [VALUE] -m [FILE/TEMPLATE] -d [START POSITION]");
                Console.ReadLine();
                return;
            }

            Searcher s = new Searcher(file_name, template, mode, start_disk);
            s.search_engine();
            Console.WriteLine("Done");
            Console.ReadLine();
        }
    }
}
