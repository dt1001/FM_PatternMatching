using System;
using System.Collections.Generic;
using System.Text;

namespace FM_PatternMatching
{
    public interface IPatterSearcher
    {
        Dictionary<string, int> Search(PatternInput query);
    }

    public class PatternSearch : IPatterSearcher
    {
        private readonly IPatternProcessor<PatternInput> patternProcessor;
        private readonly IInputValidator<PatternInput> inputValidator;

        public PatternSearch(IPatternProcessor<PatternInput> patternProcessor,
            IInputValidator<PatternInput> inputValidator)
        {
            this.patternProcessor = patternProcessor;
            this.inputValidator = inputValidator;
        }

        public Dictionary<string, int> Search(PatternInput query)
        {
            inputValidator.ValidateInput(query);
            return patternProcessor.FindPatterns(query);
        }
    }
}
