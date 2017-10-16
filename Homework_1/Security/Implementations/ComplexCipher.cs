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
