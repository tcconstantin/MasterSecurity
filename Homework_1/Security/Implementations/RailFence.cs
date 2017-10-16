/**************************************************************************
 *                                                                        *
 *  File:        RailFence.cs                                             *
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
using System.Text;

namespace Security.Implementations
{
    public class RailFence : ICrypto
    {
        private char[][] _matrix;
        private Reader _reader;
        private RailFenceOption _option;

        public RailFence(RailFenceOption option)
        {
            this._matrix = new char[option.LenBlock][];
            this._reader = new Reader(option.PathInputFile, option.LenBlock);
            this._option = option;

            if (File.Exists(_option.PathCryptoFile)) File.Delete(_option.PathCryptoFile);
            if (File.Exists(_option.PathDecryptFile)) File.Delete(_option.PathDecryptFile);
        }

        private void CreateMatrix()
        {
            _reader.Blocks = new List<string>();
            var blocks = _reader.ReadFile();
            List<List<char>> text = new List<List<char>>();
            int index = 0, i = 0;
            for (i = 0; i < _option.LenBlock; ++i) text.Add(new List<char>());

            for (i = 0; i < blocks.Count; ++i)
            {
                foreach (var currentChar in blocks[i])
                {
                    var rem = index % _option.LenBlock;
                    text[rem].Add(currentChar);
                    index++;
                }
            }

            for(i = 0; i < _option.LenBlock; ++i)
            {
                this._matrix[i] = text[i].ToArray();
            }
        }

        public void Crypt()
        {
            CreateMatrix();

            int i = 0, j = 0;
            using (StreamWriter streamWriter = File.AppendText(_option.PathCryptoFile))
            {
                for (i = 0; i < _option.LenBlock; ++i)
                {
                    for (j = 0; j < _matrix[i].Length; ++j)
                    {
                        streamWriter.Write(_matrix[i][j]);
                        
                    }
                }
            }
        }

        public void Decrypt()
        {
            _reader.Path = _option.PathCryptBookCipher;
            _reader.LenBlock = this._matrix[0].Length;
            _reader.Blocks = new List<string>();
            var blocks = _reader.ReadFile();
            char[] fullText = new char[blocks.Count * blocks[0].Length];

            for(int i = 0; i < blocks.Count; ++i)
            {
                for(int j = 0; j < blocks[i].Length; ++j)
                {
                    fullText[j * _option.LenKey + i] = blocks[i][j];
                }
            }

            using (StreamWriter streamWriter = File.AppendText(_option.PathDecryptFile))
            {
                streamWriter.Write(fullText);
            }
        }
    }
}
