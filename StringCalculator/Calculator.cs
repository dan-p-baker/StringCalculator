using System.Linq;

namespace StringCalculator
{
    public class Calculator : ICalculatorV1
    {
        private INumberServiceV1 _numberService; 
        public Calculator(INumberServiceV1 numberService)
        {
            _numberService = numberService;
        }

        int ICalculatorV1.Add(string numbers)
        {
            if (string.IsNullOrEmpty(numbers))
                return 0;

            var numbersList = _numberService.GetNumbersListFromInput(numbers);
            _numberService.ValidateAgainstNegativeNumbers(numbersList);

            return numbersList.Sum();
        }
    }
}
