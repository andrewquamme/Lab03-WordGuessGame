using System;
using System.IO;

namespace WordGuessGame
{
    public class Program
    {
        static void Main(string[] args)
        {
            string path = "../../../../words.txt";
            
            if (!File.Exists(path))
            {
                FileCreateWords(path);
            }

            for (int i = 0; i < 20; i++)
            {
                Console.WriteLine(GetRandomWord(path));
            }
        }

        static void FileCreateWords(string path)
        {
            using(StreamWriter streamwriter = new StreamWriter(path))
            {
                streamwriter.WriteLine("Dog\nCat\nFerret");
            }
        }

        static string[] FileReadWords(string path)
        {
            return File.ReadAllLines(path);
        }

        static void FileAppendWord(string path)
        {
            using (StreamWriter streamWriter = File.AppendText(path))
            {
                //streamWriter.WriteLine(DateTime.UtcNow);
            }
        }

        static void FileDeleteWords(string path)
        {
            File.Delete(path);
        }

        static int RandomNumberGenerator(int upper)
        {
            Random rand = new Random();
            return rand.Next(0, upper);
        }

        static string GetRandomWord(string path)
        {
            string[] words = FileReadWords(path);
            return words[RandomNumberGenerator(words.Length)];
        }
    }
}
