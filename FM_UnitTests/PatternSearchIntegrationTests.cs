using FM_PatternMatching;
using NUnit.Framework;
using System;

namespace FM_UnitTests
{
    class PatternSearchIntegrationTests
    {
        PatternProcessor patternProcessor;
        InputValidator queryValidator;
        PatternSearch patternSearch;

        [SetUp]
        public void SetUp()
        {
            patternProcessor = new PatternProcessor();
            queryValidator = new InputValidator();
            patternSearch = new PatternSearch(patternProcessor, queryValidator);
        }

        [Test]
        public void PatternSearch_Search_NoExceptions()
        {
            string str = Guid.NewGuid().ToString();

            PatternInput patternInput = new PatternInput
            {
                PatternLength = str.Length,
                Str = str
            };
            Assert.DoesNotThrow(() => patternSearch.Search(patternInput));
        }

        [TestCase("")]
        [TestCase(null)]
        public void PatternSearch_Search_InvalidStr_ExpectThrows(string str)
        {
            PatternInput patternInput = new PatternInput
            {
                PatternLength = 1,
                Str = str
            };
            Assert.Throws<InvalidOperationException>(() => patternSearch.Search(patternInput));
        }

        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(-100)]
        public void PatternSearch_Search_InvalidPatternLength_ExpectThrows(int PatternLength)
        {
            PatternInput patternInput = new PatternInput
            {
                PatternLength = PatternLength,
                Str = Guid.NewGuid().ToString()
            };
            Assert.Throws<InvalidOperationException>(() => patternSearch.Search(patternInput));
        }

        [Test]
        public void PatternSearch_Search_StrLengthLessThanPatternLength_ExpectThrows()
        {
            string str = Guid.NewGuid().ToString();

            PatternInput patternInput = new PatternInput
            {
                PatternLength = str.Length + 1,
                Str = str
            };
            Assert.Throws<InvalidOperationException>(() => patternSearch.Search(patternInput));
        }

        [Test]
        public void PatternSearch_Search_SinglePattern()
        {
            string pattern = "xyz";

            PatternInput patternInput = new PatternInput
            {
                PatternLength = pattern.Length,
                Str = $"{pattern}=XD{pattern}"
            };

            var results = patternSearch.Search(patternInput);

            Assert.AreEqual(2, results[pattern]);
            Assert.AreEqual(1, results.Count);
        }

        /// <summary>
        /// Given by client
        /// </summary>
        [Test]
        public void PatternSearch_Search_ThreePatterns()
        {
            string pattern1 = "abx";
            string pattern2 = "zf3";
            string pattern3 = "bxc";

            PatternInput patternInput = new PatternInput
            {
                PatternLength = 3,
                Str = $"zf3kabxcde224lkzf3mabxc51+crsdtzf3nab="
            };

            var results = patternSearch.Search(patternInput);

            Assert.AreEqual(2, results[pattern1]);
            Assert.AreEqual(3, results[pattern2]);
            Assert.AreEqual(2, results[pattern3]);
            Assert.AreEqual(3, results.Count);
        }

        [Test]
        public void PatternSearch_Search_LengthEqualStr_NoPatterns()
        {
            string str = Guid.NewGuid().ToString();
            PatternInput patternInput = new PatternInput
            {
                PatternLength = str.Length,
                Str = str
            };

            var results = patternSearch.Search(patternInput);
            Assert.AreEqual(0, results.Count);
        }

        [Test]
        public void PatternSearch_Search_LongPattern()
        {
            string pattern1 = "==========";

            PatternInput patternInput = new PatternInput
            {
                PatternLength = pattern1.Length,
                Str = $"={pattern1}=xyu&*()${pattern1}hhxpzadywlxahsjx{pattern1}==="
            };

            var results = patternSearch.Search(patternInput);
            Assert.AreEqual(3, results[pattern1]);
            Assert.AreEqual(1, results.Count);
        }

        [Test]
        public void PatternSearch_Search_ClosePatterns()
        {
            string pattern1 = "xxxxx";

            PatternInput patternInput = new PatternInput
            {
                PatternLength = pattern1.Length,
                Str = $"x{pattern1}=+==${pattern1}hhxpzadywlxahsjx{pattern1}+{pattern1}"
            };

            var results = patternSearch.Search(patternInput);
            Assert.AreEqual(4, results[pattern1]);
            Assert.AreEqual(1, results.Count);
        }

        [Test]
        public void PatternSearch_Search_NeighbouringPatterns()
        {
            string pattern1 = "xxxxx";

            PatternInput patternInput = new PatternInput
            {
                PatternLength = pattern1.Length,
                Str = $"{pattern1}{pattern1}"
            };

            var results = patternSearch.Search(patternInput);
            Assert.AreEqual(2, results[pattern1]);
            Assert.AreEqual(1, results.Count);
        }

        [Test]
        public void PatternSearch_Search_CaseInsensitivePattern()
        {
            string pattern1 = "ABC%";

            PatternInput patternInput = new PatternInput
            {
                PatternLength = pattern1.Length,
                Str = $"{pattern1}{pattern1.ToLower()}"
            };

            var results = patternSearch.Search(patternInput);
            Assert.AreEqual(2, results[pattern1.ToLower()]);
            Assert.AreEqual(1, results.Count);
        }

        [Test]
        public void PatternSearch_Search_CaseSensitivePattern()
        {
            string pattern1 = "ABC%";

            PatternInput patternInput = new PatternInput
            {
                PatternLength = pattern1.Length,
                Str = $"{pattern1}{pattern1.ToLower()}&&&&{pattern1}",
                IgnoreCase = false
            };

            var results = patternSearch.Search(patternInput);
            Assert.AreEqual(2, results[pattern1]);
            Assert.AreEqual(1, results.Count);
        }

        [Test]
        public void PatternSearch_Search_SpecialCharPattern()
        {
            string pattern1 = "#&*@&";

            PatternInput patternInput = new PatternInput
            {
                PatternLength = pattern1.Length,
                Str = $"{pattern1}{pattern1.ToLower()}!!()$*@&^{pattern1}",
                IgnoreCase = false
            };

            var results = patternSearch.Search(patternInput);
            Assert.AreEqual(3, results[pattern1]);
            Assert.AreEqual(1, results.Count);
        }
    }
}
