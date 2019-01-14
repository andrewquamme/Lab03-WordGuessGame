using System;
using Xunit;
using System.IO;
using WordGuessGame;

namespace WordGuessGameTDD
{
    public class TestStringConverters
    {
        [Fact]
        public void CanStringToIntWork()
        {
            Assert.Equal(3, Program.StringToInt("3"));
        }

        [Fact]
        public void CanStringToIntCatchLetters()
        {
            Assert.Equal(0, Program.StringToInt("x"));
        }

        [Fact]
        public void CanStringToLetterWork()
        {
            Assert.Equal('F', Program.StringToLetter("f"));
        }

        [Fact]
        public void CanStringToLetterCatchNumbers()
        {
            Assert.Equal('0', Program.StringToLetter("8"));
        }

        [Fact]
        public void CanStringToLetterCatchBlanks()
        {
            Assert.Equal('0', Program.StringToLetter(""));
        }

        [Fact]
        public void CanMakeHiddenWordWork()
        {
            string[] output = { "_", "_" };
            Assert.Equal(output, Program.MakeHiddenWord("hi"));
        }
    }

    public class TestSystemIO
    {
        [Fact]
        public void CanWriteFile()
        {
            string path = "../../../../test.txt";
            string[] words = { "testOne", "testTwo" };
            Program.WriteWordsFile(path, words);
            Assert.True(File.Exists(path));
        }

        [Fact]
        public void CanReadFile()
        {
            string path = "../../../../test2.txt";
            string[] words = { "testOne", "testTwo" };
            Program.WriteWordsFile(path, words);
            Assert.Equal(words, Program.ReadWordsFile(path));
        }

        [Fact]
        public void CanAppendFile()
        {
            string path = "../../../../test.txt";
            string[] words = { "testOne", "testTwo", "testThree" };
            Program.AppendWordFile(path, "testThree");
            Assert.Equal(words, Program.ReadWordsFile(path));
        }

        [Fact]
        public void CanDeleteWord()
        {
            string path = "../../../../test.txt";
            string[] words = { "testOne", "testThree" };
            Program.DeleteWord(path, "testTwo");
            Assert.Equal(words, Program.ReadWordsFile(path));
        }
    }

    public class TestWordChecker
    {
        [Fact]
        public void LetterExists()
        {
            string word = "test";
            char letter = 't';
            Assert.True(Program.WordContainsLetter(word, letter));
        }

        [Fact]
        public void LetterDoesNotExists()
        {
            string word = "test";
            char letter = 'x';
            Assert.False(Program.WordContainsLetter(word, letter));
        }
    }
}
