using Security.CustomException;
using System;
using System.Collections.Generic;
using System.Text;

namespace Security.Utils
{
    public class Validator
    {
        public static void ValidatePositiveNumber(int number)
        {
            if (number <= 0)
                throw new SecurityPositiveNumber(number);
        }

        public static void ValidateEvenNumber(int number)
        {
            if (number % 2 != 0 && number % 4 != 0) // % 4 for base64
                throw new SecurityEvenNumber(number);
        }
    }
}
