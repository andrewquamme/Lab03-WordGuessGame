using System;
using System.IO;

namespace WordGuessGame
{
    public class Program
    {
        static void Main(string[] args)
        {
            string path = "../../../../words.txt";
            string[] words = { "Dog", "Cat", "Ferret", "Cow", "Horse", "Chicken" };
            
            if (!File.Exists(path))
            {
                WriteWordsFile(path, words);
            }

            MainMenu(path);
        }

        /// <summary>
        /// Create words file
        /// </summary>
        /// <param name="path">filepath</param>
        /// <param name="words">arr of words</param>
        public static void WriteWordsFile(string path, string[] words)
        {
            using (StreamWriter streamwriter = new StreamWriter(path))
            {
                foreach (string word in words)
                {
                    streamwriter.WriteLine(word);
                }
            }
        }

        /// <summary>
        /// Read words from file
        /// </summary>
        /// <param name="path">filepath</param>
        /// <returns>string[] of words</returns>
        public static string[] ReadWordsFile(string path)
        {
            return File.ReadAllLines(path);
        }

        /// <summary>
        /// Append new word to word file
        /// </summary>
        /// <param name="path">filepath</param>
        /// <param name="word">word to add</param>
        /// <returns>string "success"</returns>
        public static string AppendWordFile(string path, string word)
        {
            using (StreamWriter streamWriter = File.AppendText(path))
            {
                streamWriter.WriteLine(word);
            }
            return "***Word added successfully***";
        }

        static void DeleteWordsFile(string path)
        {
            File.Delete(path);
        }

        public static string DeleteWord(string path, string word)
        {
            string[] oldWords = ReadWordsFile(path);
            string[] newWords = new string[oldWords.Length - 1];
            int j = 0;
            for (int i = 0; i < oldWords.Length; i++)
            {
                try
                {
                    if (oldWords[i].ToUpper() != word.ToUpper())
                    {
                        newWords[j] = oldWords[i];
                        j++;
                    }
                    WriteWordsFile(path, newWords);
                }
                catch (IndexOutOfRangeException)
                {
                    return "***Word not found***";
                }
            }
            return "***Word deleted successfully***";
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
                        AdminMenu(path);
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

        static void AdminMenu(string path)
        {
            bool running = true;
            string wordEntry = "";
            do
            {
                Console.Clear();
                Console.WriteLine("Word Guessing Game!\nAdmin Menu");
                Console.WriteLine("1. Add Word");
                Console.WriteLine("2. Delete Word");
                Console.WriteLine("3. Return to Main Menu");
                Console.Write("Select an option: ");
                string userSelection = Console.ReadLine();

                int selection = StringToInt(userSelection);
                switch (selection)
                {
                    case 1:
                        Console.Write("Enter word to add: ");
                        wordEntry = Console.ReadLine();
                        string newWord = wordEntry;
                        Console.WriteLine(AppendWordFile(path, newWord) + "\nPress any key to continue...");
                        Console.ReadLine();
                        break;
                    case 2:
                        Console.Write("Enter word to delete: ");
                        wordEntry = Console.ReadLine();
                        string deleteWord = wordEntry;
                        Console.Write(DeleteWord(path, deleteWord) + "\nPress any key to continue...");
                        Console.ReadLine();
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
            string[] words = ReadWordsFile(path);
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

                //if letter has not been guessed and is only a letter
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
            Console.Write("Great work!\nPress any key to return to Main Menu...");
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
