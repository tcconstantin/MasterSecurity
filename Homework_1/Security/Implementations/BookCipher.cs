/**************************************************************************
 *                                                                        *
 *  File:        BookCipher.cs                                            *
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

using Security.Implementations.Options;
using Security.Interfaces;
using Security.Utils.FileHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Security.Implementations
{
    public class BookCipher : ICrypto
    {
        private Dictionary<string, int> _book;
        private Reader _reader;
        private BookCipherOption _option;
        private Random _random = new Random();
        private int _digits;

        public BookCipher(BookCipherOption option)
        {
            this._book = new Dictionary<string, int>();
            this._option = option;
            this._reader = new Reader(option.PathInputFile, option.LenBlock);
            this._random = new Random();
            this._digits = NumberOfDigits(option.MaxNumber);


            if (File.Exists(_option.PathCryptoFile)) File.Delete(_option.PathCryptoFile);
            if (File.Exists(_option.PathDecryptFile)) File.Delete(_option.PathDecryptFile);
        }

        private int NumberOfDigits(int number)
        {
            int contor = 0;
            while(number > 0)
            {
                number /= 10;
                contor++;
            }

            return contor;
        }

        public void Crypt()
        {
            var blocks = _reader.ReadFile();
            var pow = (int)Math.Pow(10, _digits - 1);
            using (StreamWriter streamWriter = File.AppendText(_option.PathCryptoFile))
            {
                for (int i = 0; i < blocks.Count; ++i)
                {
                    var number = 0;
                    var exits = false;
                    do
                    {
                        number = _random.Next(pow, _option.MaxNumber);
                        exits = _book.ContainsValue(number);
                    } while (exits);

                    _book.Add(blocks[i], number);
                    streamWriter.Write(number);
                }
            }
        }

        public void Decrypt()
        {
            _reader.Blocks = new List<string>();
            _reader.Path = _option.PathCryptoFile;
            _reader.LenBlock = _digits;
            var blocks = _reader.ReadFile();
            using (StreamWriter streamWriter = File.AppendText(_option.PathDecryptFile))
            {
                for (int i = 0; i < blocks.Count; ++i)
                {
                    var value = int.Parse(blocks[i]);
                    var key = _book.FirstOrDefault(x => x.Value == value).Key;
                    streamWriter.Write(key);
                }
            }
        }
    }
}
