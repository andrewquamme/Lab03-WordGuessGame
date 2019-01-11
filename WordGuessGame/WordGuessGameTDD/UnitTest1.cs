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
    }
}
