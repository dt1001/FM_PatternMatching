using System;
using System.Collections.Generic;

namespace FM_PatternMatching
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length >= 2 &&
                int.TryParse(args[1], out int patternLength))
            {
                string str = args[0];
                FindPatterns(str, patternLength);
            }
        }

        public static void FindPatterns(string str, int patternLength, bool ignoreCase = true)
        {
            PatternProcessor patternProcessor = new PatternProcessor();
            InputValidator queryValidator = new InputValidator();
            PatternSearch patternSearch = new PatternSearch(patternProcessor, queryValidator);

            PatternInput patternQuery = new PatternInput
            {
                PatternLength = patternLength,
                Str = str,
                IgnoreCase = ignoreCase
            };

            try
            {
                var results = patternSearch.Search(patternQuery);

                foreach (var key in results.Keys)
                {
                    Console.WriteLine($"{key}: {results[key]}");
                }

                Console.WriteLine("process complete...");
            }
            catch(InvalidOperationException ex)
            {
                Console.WriteLine($"Invalid input: {ex.Message}");
            }
            
            Console.Read();
        }
    }
}
