﻿/**************************************************************************
 *                                                                        *
 *  File:        BaseOption.cs                                            *
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
using System.Collections.Generic;
using System.Text;

namespace Security.Implementations.Options
{
    public class BaseOption
    {
        public string PathInputFile { get; set; }
        public string PathCryptoFile { get; set; }
        public string PathDecryptFile { get; set; }
    }
}
