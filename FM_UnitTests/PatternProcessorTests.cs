using FM_PatternMatching;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace FM_UnitTests
{
    [TestFixture]
    class PatternProcessorTests
    {
        PatternProcessor patternProcessor;

        [SetUp]
        public void SetUp()
        {
            patternProcessor = new PatternProcessor();
        }

        [Test]
        public void PatternProcessor_FindPatterns_SinglePattern()
        {
            string pattern = "xyz";

            PatternInput patternInput = new PatternInput
            {
                PatternLength = pattern.Length,
                Str = $"{pattern}=XD{pattern}"
            };

            var results = patternProcessor.FindPatterns(patternInput);

            Assert.AreEqual(2, results[pattern]);
            Assert.AreEqual(1, results.Count);
        }

        /// <summary>
        /// Given by client
        /// </summary>
        [Test]
        public void PatternProcessor_FindPatterns_ThreePatterns()
        {
            string pattern1 = "abx";
            string pattern2 = "zf3";
            string pattern3 = "bxc";

            PatternInput patternInput = new PatternInput
            {
                PatternLength = 3,
                Str = $"zf3kabxcde224lkzf3mabxc51+crsdtzf3nab="
            };

            var results = patternProcessor.FindPatterns(patternInput);

            Assert.AreEqual(2, results[pattern1]);
            Assert.AreEqual(3, results[pattern2]);
            Assert.AreEqual(2, results[pattern3]);
            Assert.AreEqual(3, results.Count);
        }

        [Test]
        public void PatternProcessor_FindPatterns_LengthEqualStr_NoPatterns()
        {
            string str = Guid.NewGuid().ToString();
            PatternInput patternInput = new PatternInput
            {
                PatternLength = str.Length,
                Str = str
            };

            var results = patternProcessor.FindPatterns(patternInput);
            Assert.AreEqual(0, results.Count);
        }

        [Test]
        public void PatternProcessor_FindPatterns_LongPattern()
        {
            string pattern1 = "==========";

            PatternInput patternInput = new PatternInput
            {
                PatternLength = pattern1.Length,
                Str = $"={pattern1}=xyu&*()${pattern1}hhxpzadywlxahsjx{pattern1}==="
            };

            var results = patternProcessor.FindPatterns(patternInput);
            Assert.AreEqual(3, results[pattern1]);
            Assert.AreEqual(1, results.Count);
        }

        [Test]
        public void PatternProcessor_FindPatterns_ClosePatterns()
        {
            string pattern1 = "xxxxx";

            PatternInput patternInput = new PatternInput
            {
                PatternLength = pattern1.Length,
                Str = $"x{pattern1}=+==${pattern1}hhxpzadywlxahsjx{pattern1}+{pattern1}"
            };

            var results = patternProcessor.FindPatterns(patternInput);
            Assert.AreEqual(4, results[pattern1]);
            Assert.AreEqual(1, results.Count);
        }

        [Test]
        public void PatternProcessor_FindPatterns_NeighbouringPatterns()
        {
            string pattern1 = "xxxxx";

            PatternInput patternInput = new PatternInput
            {
                PatternLength = pattern1.Length,
                Str = $"{pattern1}{pattern1}"
            };

            var results = patternProcessor.FindPatterns(patternInput);
            Assert.AreEqual(2, results[pattern1]);
            Assert.AreEqual(1, results.Count);
        }

        [Test]
        public void PatternProcessor_FindPatterns_CaseInsensitivePattern()
        {
            string pattern1 = "ABC%";

            PatternInput patternInput = new PatternInput
            {
                PatternLength = pattern1.Length,
                Str = $"{pattern1}{pattern1.ToLower()}"
            };

            var results = patternProcessor.FindPatterns(patternInput);
            Assert.AreEqual(2, results[pattern1.ToLower()]);
            Assert.AreEqual(1, results.Count);
        }

        [Test]
        public void PatternProcessor_FindPatterns_CaseSensitivePattern()
        {
            string pattern1 = "ABC%";

            PatternInput patternInput = new PatternInput
            {
                PatternLength = pattern1.Length,
                Str = $"{pattern1}{pattern1.ToLower()}&&&&{pattern1}",
                IgnoreCase = false
            };

            var results = patternProcessor.FindPatterns(patternInput);
            Assert.AreEqual(2, results[pattern1]);
            Assert.AreEqual(1, results.Count);
        }

        [Test]
        public void PatternProcessor_FindPatterns_SpecialCharPattern()
        {
            string pattern1 = "#&*@&";

            PatternInput patternInput = new PatternInput
            {
                PatternLength = pattern1.Length,
                Str = $"{pattern1}{pattern1.ToLower()}!!()$*@&^{pattern1}",
                IgnoreCase = false
            };

            var results = patternProcessor.FindPatterns(patternInput);
            Assert.AreEqual(3, results[pattern1]);
            Assert.AreEqual(1, results.Count);
        }
    }
}
