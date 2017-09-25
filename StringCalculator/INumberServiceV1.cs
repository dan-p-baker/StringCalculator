using System.Collections.Generic;

namespace StringCalculator
{
    public interface INumberServiceV1
    {        
        List<int> GetNumbersListFromInput(string numbers);
        void ValidateAgainstNegativeNumbers(List<int> numbersList);
    }
}
