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
            using (StreamWriter streamwriter = new StreamWriter(path))
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

        static string GetRandomWord(string path)
        {
            string[] words = FileReadWords(path);
            return words[RandomNumberGenerator(words.Length)].ToUpper();
        }

        static void PlayGame(string word)
        {
            char[] letterArray = word.ToCharArray();
            string[] hiddenWord = MakeHiddenWord(word);
            int correct = 0;
            string guesses = "";

            do
            {
                ShowWordAndLettersGuessed(hiddenWord, guesses);
                Console.Write("Guess a letter: ");
                string userInput = Console.ReadLine();
                char letter = StringToLetter(userInput);

                if (!guesses.Contains(letter) && letter != '0')
                {
                    guesses += letter;
                    if (word.Contains(letter))
                    {
                        for (int i = 0; i < letterArray.Length; i++)
                        {
                            if (letterArray[i] == letter)
                            {
                                hiddenWord[i] = " " + letter;
                                correct++;
                            }
                        }
                    }
                }
            } while (correct < word.Length);

            ShowWordAndLettersGuessed(hiddenWord, guesses);
            Console.Write("Great work!\nPress any key to return to Main Menu");
            Console.ReadLine();
        }

        static int RandomNumberGenerator(int upper)
        {
            Random rand = new Random();
            return rand.Next(0, upper);
        }

        public static string[] MakeHiddenWord(string word)
        {
            string[] hiddenWord = new string[word.Length];
            for (int i = 0; i < word.Length; i++)
            {
                hiddenWord[i] = "_";
            }
            return hiddenWord;
        }

        static void ShowWordAndLettersGuessed(string[] hiddenWord, string guesses)
        {
            Console.Clear();
            Console.WriteLine(string.Join(" ", hiddenWord));
            Console.WriteLine("Letters Guessed: " + guesses);
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

        public static char StringToLetter(string input)
        {
            char letter;
            try
            {
                letter = Convert.ToChar(input.ToUpper());
            }
            catch (Exception)
            {
                return '0';
            }
            if (Char.IsLetter(letter)) {
                return letter;
            }
            return '0';
        }
    }
}
