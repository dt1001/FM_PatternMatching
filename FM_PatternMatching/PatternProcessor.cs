using System;
using System.Collections.Generic;

namespace FM_PatternMatching
{
    public interface IPatternProcessor<TInput>
    {
        Dictionary<string, int> FindPatterns(TInput input);
    }

    public class PatternProcessor : IPatternProcessor<PatternInput>
    {
        /// <summary>
        /// Finds all patterns in the given str of the given PatternLength
        /// that occur more than once
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Dictionary<string, int> FindPatterns(PatternInput input)
        {
            string str;
            if (input.IgnoreCase)
            {
                str = input.Str.ToLower();
            }
            else
            {
                str = input.Str;
            }

            Dictionary<string, int> results = new Dictionary<string, int>(); 
            
            string pattern;
            string previousPattern = string.Empty;
            int previousPatternIndex = 0;

            for(int i = 0; i < (str.Length - input.PatternLength + 1); i++)
            {
                pattern = str.Substring(i, input.PatternLength);

                if (results.ContainsKey(pattern))
                {
                    //prevent the same overlapping pattern from being counted
                    if(!pattern.Equals(previousPattern, StringComparison.InvariantCultureIgnoreCase) ||
                        i >= (previousPatternIndex + input.PatternLength) )
                    {
                        results[pattern]++;
                        previousPatternIndex = i;
                    }
                }
                else
                {
                    results.Add(pattern, 1);
                }
                previousPattern = pattern;
            }

            RemoveInvalidKvp(results);

            return results;
        }

        /// <summary>
        /// Remove all key value pairs that have a value of less than 2
        /// </summary>
        private void RemoveInvalidKvp(Dictionary<string, int> results)
        {
            foreach (var key in results.Keys)
            {
                if(results[key] < 2)
                {
                    results.Remove(key);
                }
            }
        }
    }
}
