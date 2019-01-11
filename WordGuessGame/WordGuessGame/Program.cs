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
                //Write words file if does not exist
            {
                FileCreateWords(path);
            }

            MainMenu(path);
        }

        /// <summary>
        /// Writes word file
        /// </summary>
        /// <param name="path">file path</param>
        static void FileCreateWords(string path)
        {
            using(StreamWriter streamwriter = new StreamWriter(path))
            {
                streamwriter.WriteLine("Dog\nCat\nFerret\nCow\nHorse\nAlpaca\nChicken");
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

        static void MainMenu(string path)
        {
            bool running = true;
            do
            {
                Console.Clear();
                Console.WriteLine("Word Guessing Game!\nMain Menu");
                Console.WriteLine("1. Play Game");
                Console.WriteLine("2. Admin");
                Console.WriteLine("3. Quit");
                Console.Write("Select an option: ");
                string userSelection = Console.ReadLine();

                int selection = StringToInt(userSelection);
                switch (selection)
                {
                    case 1:
                        PlayGame(GetRandomWord(path));
                        break;
                    case 2:
                        break;
                    case 3:
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Please make a valid selection\nPress any key to continue...");
                        Console.ReadLine();
                        break;
                }

            } while (running);
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
            Console.Write("Great work!\nPress any key to return to Main Menu");
            Console.ReadLine();
        }

        static void ShowWordAndLettersGuessed(string[] gameWord, string guesses)
        {
            Console.Clear();
            Console.WriteLine(string.Join(" ", gameWord));
            Console.WriteLine("Letters Guessed:" + guesses);
        }

        public static int StringToInt(string input)
        {
            try
            {
                return Convert.ToInt32(input);
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}
