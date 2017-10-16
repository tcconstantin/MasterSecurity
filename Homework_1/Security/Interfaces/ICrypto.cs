using Security.Utils.FileHelper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Security.Interfaces
{
    public interface ICrypto
    {
        void Crypt();

        void Decrypt();
    }
}
