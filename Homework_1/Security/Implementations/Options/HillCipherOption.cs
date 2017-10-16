using System;
using System.Collections.Generic;
using System.Text;

namespace Security.Implementations.Options
{
    public class HillCipherOption : BaseOption
    {
        public int LenBlock { get; set; }
        public int Mod { get; set; }
        public string PathCryptRailFence { get; set; }
    }
}
