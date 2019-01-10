using System;
using System.IO;

namespace WordGuessGame
{
    public class Program
    {
        static void Main(string[] args)
        {
            string path = "../../../../words.txt";
            if (File.Exists(path))
            {
                FileReadWords(path);
            }
            else
            {
                FileCreateWords(path);
            }
            FileAppendWord(path);
            //FileDeleteWords(path);
            
        }

        static void FileCreateWords(string path)
        {
            using(StreamWriter streamwriter = new StreamWriter(path))
            {
                streamwriter.WriteLine("Dog\nCat\nFerret");
            }
        }

        static void FileReadWords(string path)
        {
            string[] words = File.ReadAllLines(path);
            for (int i = 0; i < words.Length; i++)
            {
                Console.WriteLine(words[i]);
            }
        }

        static void FileAppendWord(string path)
        {
            using (StreamWriter streamWriter = File.AppendText(path))
            {
                streamWriter.WriteLine(DateTime.UtcNow);
            }
        }

        static void FileDeleteWords(string path)
        {
            File.Delete(path);
        }

        static void RandomNumberGenerator()
        {

        }
    }
}
