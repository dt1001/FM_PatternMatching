using System;
using System.Collections.Generic;
using System.Text;

namespace FM_PatternMatching
{
    public interface IInputValidator<TInput>
    {
        void ValidateInput(TInput input);
    }

    public abstract class InputValidatorBase<TInput> : IInputValidator<TInput>
    {
        public abstract void ValidateInput(TInput input);

        /// <summary>
        /// Asserts that the condition is true.
        /// Throws InvalidOperationException with errorMessage if condition is false
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="errorMessage"></param>
        protected void Assert(bool condition, string errorMessage)
        {
            if (!condition)
            {
                throw new InvalidOperationException(errorMessage);
            }
        }
    }

    public class InputValidator : InputValidatorBase<PatternInput>
    {
        public override void ValidateInput(PatternInput input)
        {
            bool isPatternLengthValid = input.PatternLength > 0;
            Assert(isPatternLengthValid, "PatternLength must be greater than 0");

            bool isStringNullOrEmpty = string.IsNullOrEmpty(input.Str);
            Assert(!isStringNullOrEmpty, "Input string cannot be empty");

            bool isStrLengthGreaterEqPatternLength = input.Str.Length >= input.PatternLength;
            Assert(isStrLengthGreaterEqPatternLength, "Input strings length must be greater than or equal to PatternLength");
        }
    }
}
