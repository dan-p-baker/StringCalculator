using NUnit.Framework;
using StringCalculator.Framework;

namespace StringCalculator.Test
{   
    
    [TestFixture]
    public class Calculator_Tests
    {
        private static ICalculatorV1 _calculator;

        [SetUp]
        public void SetUp()
        {
            StringCalculatorTestModule.Start();
            _calculator = StringCalculatorTestModule.Container.GetInstance<ICalculatorV1>();
        }

        [TestCase("", 0, TestName = "Empty_string_returns_zero")] 
        [TestCase("1", 1, TestName = "Single_digit_returns_same_value")]
        [TestCase("1,2", 3, TestName = "Two_digits_return_correct_sum")]
        public void Scenario_one(string numbers, int expectedResult)
        {
            var result = _calculator.Add(numbers);

            Assert.AreEqual(expectedResult, result);
        }

        [TestCase("14,15,34,1,5,98,103", TestName = "Calculator_can_handle_unknown_amount_of_numbers" )]
        public void Scenario_two(string numbers)
        {
            var expectedResult = 14 + 15 + 34 + 1 + 5 + 98 + 103;
            var result = _calculator.Add(numbers);

            Assert.AreEqual(expectedResult, result);
        }

        [TestCase("1\n2,3", 6, TestName = "Calculator_can_handle_new_lines_between_numbers")]       
        public void Scenario_three_a(string numbers, int expectedResult)
        {
            var result = _calculator.Add(numbers);

            Assert.AreEqual(expectedResult, result);
        }
       
        [TestCase("1,\n", "A new line is not allowed directly after a delimeter", TestName = "Calculator_does_not_allow_new_line_directly_after_delimeter")]
        public void Scenario_three_b(string numbers, string expectedExceptionMessage)
        {
            var exception = Assert.Throws<NewLineAfterDelimeterException>(() => _calculator.Add(numbers));

            Assert.That(exception.Message == expectedExceptionMessage);
        }

        [TestCase("//;\n1;2", 3, TestName = "Calculator_can_handle_single_character_delimeters")]
        public void Scenario_four(string numbers, int expectedResult)
        {
            var result = _calculator.Add(numbers);

            Assert.AreEqual(expectedResult, result);
        }

        [TestCase("1,2,-5,6", "Negative numbers not allowed: -5", TestName = "Single_negative_value_results_in_correct_exception_message_being_thrown")]
        [TestCase("1,2,-5,-6,7", "Negative numbers not allowed: -5,-6", TestName = "Multiple_negative_values_all_included_in_exception_message")]
        public void Scenario_five(string numbers, string expectedExceptionMessage)
        {
            var exception = Assert.Throws<NegativeNumberException>(() => _calculator.Add(numbers));

            Assert.That(exception.Message == expectedExceptionMessage);
        }

        [TestCase("2,1001,13", 15, TestName = "Numbers_greater_than_1000_are_ignored")]
        public void Scenario_six(string numbers, int expectedResult)
        {
            var result = _calculator.Add(numbers);

            Assert.AreEqual(expectedResult, result);
        }

        [TestCase("//*%\n1*2%3", 6, TestName = "Multiple_delimeters_can_be_used")]
        public void Scenario_seven(string numbers, int expectedResult)
        {
            var result = _calculator.Add(numbers);

            Assert.AreEqual(expectedResult, result);
        }
    }
}
