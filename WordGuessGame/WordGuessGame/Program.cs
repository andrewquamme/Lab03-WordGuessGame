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

            PlayGame(GetRandomWord(path));
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

        static void PlayGame(string randomWord)
        {
            string word = randomWord.ToUpper();
            char[] wordArray = word.ToCharArray();
            string[] gameWord = new string[word.Length];
            int correct = 0;
            string guesses = "";

            for (int i = 0; i < word.Length; i++)
            {
                gameWord[i] = "_";
            }

            do
            {
                ShowWordAndLettersGuessed(gameWord, guesses);
                Console.Write("Guess a letter: ");
                string guessedLetter = Console.ReadLine();
                string letter = guessedLetter.ToUpper();
                if (!guesses.Contains(letter))
                {
                    guesses += letter;
                    if (word.Contains(letter))
                    {
                        for (int i = 0; i < wordArray.Length; i++)
                        {
                            if (wordArray[i] == Convert.ToChar(letter))
                            {
                                gameWord[i] = " " + letter;
                                correct++;
                            }
                        }
                    }
                }
            } while (correct < word.Length);
            ShowWordAndLettersGuessed(gameWord, guesses);
            Console.WriteLine("Great work!");
        }

        static void ShowWordAndLettersGuessed(string[] gameWord, string guesses)
        {
            Console.Clear();
            Console.WriteLine(string.Join(" ", gameWord));
            Console.WriteLine("Letters Guessed:" + guesses);
        }
    }
}
