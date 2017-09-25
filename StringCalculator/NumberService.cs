using StringCalculator.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace StringCalculator
{
    public class NumberService : INumberServiceV1
    {
        private char[] _defaultDelimeter => new[] { ',', '\n' }; 

        List<int> INumberServiceV1.GetNumbersListFromInput(string input)
        {
            var userHasDefinedDelimeter = input.StartsWith("//");

            var delimeters = userHasDefinedDelimeter
                ? GetDelimeters(input)
                : _defaultDelimeter;

            var numbers = userHasDefinedDelimeter
                ? GetNumbersWithoutDelimeter(input)
                : input;

            ValidateNoNewlLineAfterDelimeter(numbers, delimeters);

            return numbers
                .Split(delimeters, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .Where(i => i <= 1000)
                .ToList();
        }

        void INumberServiceV1.ValidateAgainstNegativeNumbers(List<int> numbersList)
        {
            var negativeNumbersList = numbersList.Where(i => i < 0);

            if (negativeNumbersList.Any())
            {
                var negativeNumbersString = string.Join(",", negativeNumbersList.ToArray());
                throw new NegativeNumberException($"Negative numbers not allowed: {negativeNumbersString}");
            }                
        }

        private void ValidateNoNewlLineAfterDelimeter(string numbers, char[] delimeters)
        {
            foreach (var delimeter in delimeters)
            {
                if (Regex.IsMatch(numbers, $@"(\{delimeter}\n)"))
                    throw new NewLineAfterDelimeterException("A new line is not allowed directly after a delimeter");
            }
        }

        private char[] GetDelimeters(string input)
        {
            return Regex
                .Match(input, @"\/\/(?<delimeter>.*)\n")
                .Groups["delimeter"]
                .Value
                .ToCharArray();
        }

        private string GetNumbersWithoutDelimeter(string input)
        {
            return Regex
                .Match(input, @"\/\/(.*)\n(?<numbers>.*)")
                .Groups["numbers"]
                .Value;                
        }
    }
}
