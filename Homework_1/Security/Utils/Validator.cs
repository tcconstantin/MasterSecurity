/**************************************************************************
 *                                                                        *
 *  File:        Validator.cs                                             *
 *  Copyright:   (c) 2017, Todireanu Constantin Catalin                   *
 *  Description: MasterSecurity-Homework_1                                *
 *                                                                        *
 *  This code and information is provided "as is" without warranty of     *
 *  any kind, either expressed or implied, including but not limited      *
 *  to the implied warranties of merchantability or fitness for a         *
 *  particular purpose. You are free to use this source code in your      *
 *  applications as long as the original copyright notice is included.    *
 *                                                                        *
 **************************************************************************/

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
