using System;
using System.Collections.Generic;
using System.Text;

namespace Security.Implementations.Options
{
    public class RailFenceOption : BaseOption
    {
        public int LenBlock { get; set; }
        public int LenKey { get; set; }
        public string PathCryptBookCipher { get; set; }
    }
}
