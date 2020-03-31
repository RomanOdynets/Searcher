using System;
using System.IO;
using System.Threading;

namespace Searcher
{
    /// <summary>Предоставляет экземпляр для поиска файла по поисковому шаблону или по части имени в указанном диске.</summary>
    class Searcher
    {
        private string file_name { get; set; }
        private string template { get; set; }
        private string mode { get; set; }
        private string starter_path { get; set; }

        /// <summary>
        /// Создает экземпляр поискового алгоритма по указанным параметрам.
        /// </summary>
        public Searcher(string file_name, string template, string mode, string starter_path)
        {
            this.file_name = file_name;
            this.template = template;
            this.mode = mode;
            this.starter_path = starter_path;
        }

        /// <summary>
        /// Проводит поиск в указанной папке и выводит информацию обратно.
        /// </summary>
        /// <param name="path"></param>
        private void search_in_dir(object path)
        {
            // Защита от ошибок безопасности директории
            try
            {
                // Проверка на существование путя
                if (!Directory.Exists((string)path)) return;

                // Буффер для массифа подфайлов
                string[] files = new string[1024];

                // Сравнение аргументов запуска
                if (mode == "file")
                {
                    // Получение массива путей файлов
                    files = Directory.GetFiles((string)path);
                }
                else if(mode == "template")
                {
                    // Получение массива путей файлов используя поисковый шаблон
                    files = Directory.GetFiles((string)path, template);
                }
                
                // Для каждого файла в массиве
                foreach (var item in files)
                {
                    // Если имя файла содержит поисковое значение
                    // Если поиск по шаблону - сразу вывод
                    if (item.Contains(file_name) && mode == "file") Console.WriteLine(item);
                    else if(mode == "template") Console.WriteLine(item);
                }
                return;
            }
            catch (Exception e) { }
            
        }

        /// <summary>
        /// Ищет файлы в папке и делает рекурсию по каждой подпапке в новом пуле поток.
        /// Просмотрите <seealso cref="ThreadPool"/> для изучения принципа работы.
        /// </summary>
        /// <param name="path"></param>
        private void search_in(object path)
        {
            // Защита от ошибок безопасности директории
            try
            {
                // Получение массива папок из пути
                string[] s = Directory.GetDirectories((string)path);
                // Перенос поиска в подпапках и текущей папке в очередь потоков
                foreach (var i in s)
                {
                    ThreadPool.QueueUserWorkItem(new WaitCallback(search_in), i);
                    ThreadPool.QueueUserWorkItem(new WaitCallback(search_in_dir), i);
                }
            }
            catch (Exception e) { }
        }

        /// <summary>
        /// Выполняет поиск с стартовой позиции, переносит дальнейший в очередь потоков
        /// Очередь потоков с помощнью <seealso cref="ThreadPool.QueueUserWorkItem(WaitCallback)"/>
        /// </summary>
        /// <param name="p"></param>
        private void search(object p)
        {
            // Защита от ошибок безопасности директории
            try
            {
                // Получение массива папок из пути
                string[] s = Directory.GetDirectories(starter_path);
                // Поиск файлов в текущей директории
                search_in_dir(starter_path);

                // Перенос поиска в подпапках и текущей папке в очередь потоков
                foreach (var i in s)
                {
                    ThreadPool.QueueUserWorkItem(new WaitCallback(search_in), i);
                    ThreadPool.QueueUserWorkItem(new WaitCallback(search_in_dir), i);
                }
            }
            catch (Exception e) { }
        }

        public void search_engine(int threads_count)
        {
            // устанавливаем количество потоков в пуле потоков на 1 логический процессор
            int MaxThreadsCount = Environment.ProcessorCount * threads_count;
            // Установим максимальное количество рабочих потоков
            ThreadPool.SetMaxThreads(MaxThreadsCount, MaxThreadsCount);
            // Установим минимальное количество рабочих потоков
            ThreadPool.SetMinThreads(2, 2);
            // Запуск метода search в очереди потоков
            ThreadPool.QueueUserWorkItem(new WaitCallback(search));
        }
    }
}
