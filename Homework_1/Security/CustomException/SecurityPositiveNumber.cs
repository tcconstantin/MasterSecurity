using System;

namespace Security.CustomException
{
    public class SecurityPositiveNumber : Exception
    {
        public SecurityPositiveNumber(int number) :
            base($"[Security] The length of the read block must be a positive number. Now it is {number}.")
        {

        }
    }
}
