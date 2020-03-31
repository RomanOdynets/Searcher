using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

namespace Searcher
{
    class Searcher
    {
        private string file_name { get; set; }
        private string template { get; set; }
        private string mode { get; set; }
        public List<string> searched = new List<string>();
        private string starter_path { get; set; }

        public Searcher(string file_name, string template, string mode, string starter_path)
        {
            this.file_name = file_name;
            this.template = template;
            this.mode = mode;
            this.starter_path = starter_path;
        }

        private void search_in_dir(object path)
        {
            try
            {
                if (!Directory.Exists((string)path)) return;

                string[] files = new string[1024];

                if (mode == "file")
                {
                    files = Directory.GetFiles((string)path);
                }
                else if(mode == "template")
                {
                    files = Directory.GetFiles((string)path, template);
                }
                
                foreach (var item in files)
                {
                    if (item.Contains(file_name))
                    {
                        Console.WriteLine(item);
                    }
                }
                return;
            }
            catch (Exception e) { }
            
        }

        private void search_in(object path)
        {
            try
            {
                string[] s = Directory.GetDirectories((string)path);
                foreach (var i in s)
                {
                    ThreadPool.QueueUserWorkItem(new WaitCallback(search_in), i);
                    ThreadPool.QueueUserWorkItem(new WaitCallback(search_in_dir), i);
                }
            }
            catch (Exception e) { }
        }

        private void search(object p)
        {
            try
            {
                string[] s = Directory.GetDirectories(starter_path);
                search_in_dir(starter_path);


                foreach (var i in s)
                {
                    ThreadPool.QueueUserWorkItem(new WaitCallback(search_in), i);
                    ThreadPool.QueueUserWorkItem(new WaitCallback(search_in_dir), i);
                }
            }
            catch (Exception e) { }
        }

        public void search_engine()
        {
            int MaxThreadsCount = Environment.ProcessorCount * 16;
            // Установим максимальное количество рабочих потоков
            ThreadPool.SetMaxThreads(MaxThreadsCount, MaxThreadsCount);
            // Установим минимальное количество рабочих потоков
            ThreadPool.SetMinThreads(2, 2);
            ThreadPool.QueueUserWorkItem(new WaitCallback(search));
        }
    }
}
