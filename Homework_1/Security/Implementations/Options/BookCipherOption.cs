using System;
using System.Collections.Generic;
using System.Text;

namespace Security.Implementations.Options
{
    public class BookCipherOption : BaseOption
    {
        public int LenBlock { get; set; }
        public int MaxNumber { get; set; }
    }
}
