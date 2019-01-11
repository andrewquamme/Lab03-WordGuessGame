using System;
using Xunit;
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
}
