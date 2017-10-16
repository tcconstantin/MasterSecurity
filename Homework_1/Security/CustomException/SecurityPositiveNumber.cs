/**************************************************************************
 *                                                                        *
 *  File:        SecurityPositiveNumber.cs                                *
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
