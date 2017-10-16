using System;
using System.Collections.Generic;
using System.Text;

namespace Security.CustomException
{
    public class SecurityEvenNumber : Exception
    {
        public SecurityEvenNumber(int number) :
            base($"[Security] The length of the read block must be a even number. Now it is {number}.")
        {

        }
    }
}
