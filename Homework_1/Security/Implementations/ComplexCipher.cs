/**************************************************************************
 *                                                                        *
 *  File:        ComplexCipher.cs                                         *
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

using Security.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Security.Implementations
{
    public class ComplexCipher : ICrypto
    {
        private IList<ICrypto> _algorithms;

        public ComplexCipher(IList<ICrypto> algorithms)
        {
            this._algorithms = algorithms;

        }

        public void Crypt()
        {
            for (int i = 0; i < _algorithms.Count; ++i)
                _algorithms[i].Crypt();
        }

        public void Decrypt()
        {
            for (int i = _algorithms.Count - 1; i >= 0; --i)
                _algorithms[i].Decrypt();
        }
    }
}
