using FM_PatternMatching;
using NUnit.Framework;
using System;

namespace FM_UnitTests
{
    [TestFixture]
    public class InputValidatorTests
    {
        InputValidator inputValidator;

        [SetUp]
        public void Setup()
        {
            inputValidator = new InputValidator();
        }

        [Test]
        public void InputValidator_ValidateInput_NoExceptions()
        {
            string str = Guid.NewGuid().ToString();

            PatternInput patternInput = new PatternInput
            {
                PatternLength = str.Length,
                Str = str
            };
            Assert.DoesNotThrow(() => inputValidator.ValidateInput(patternInput));
        }

        [TestCase("")]
        [TestCase(null)]
        public void InputValidator_ValidateInput_InvalidStr_ExpectThrows(string str)
        {
            PatternInput patternInput = new PatternInput
            {
                PatternLength = 1,
                Str = str
            };
            Assert.Throws<InvalidOperationException>(() => inputValidator.ValidateInput(patternInput));
        }

        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(-100)]
        public void InputValidator_ValidateInput_InvalidPatternLength_ExpectThrows(int PatternLength)
        {
            PatternInput patternInput = new PatternInput
            {
                PatternLength = PatternLength,
                Str = Guid.NewGuid().ToString()
            };
            Assert.Throws<InvalidOperationException>(() => inputValidator.ValidateInput(patternInput));
        }

        [Test]
        public void InputValidator_ValidateInput_StrLengthLessThanPatternLength_ExpectThrows()
        {
            string str = Guid.NewGuid().ToString();

            PatternInput patternInput = new PatternInput
            {
                PatternLength = str.Length + 1,
                Str = str
            };
            Assert.Throws<InvalidOperationException>(() => inputValidator.ValidateInput(patternInput));
        }
    }
}