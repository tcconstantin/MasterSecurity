/**************************************************************************
 *                                                                        *
 *  File:        Reader.cs                                                *
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

using Security.CustomException;
using System;
using System.Collections.Generic;
using System.IO;

namespace Security.Utils.FileHelper
{
    public class Reader
    {
        private string _path;
        private int _length;
        private List<string> _blocks;

        public Reader(string path, int length)
        {
            _path = path;
            _length = length;
            _blocks = new List<string>();

            Validator.ValidatePositiveNumber(length);
            Validator.ValidateEvenNumber(length);
        }

        public string Path
        {
            set
            {
                _path = value;
            }
            get
            {
                return _path;
            }
        }

        public List<string> Blocks
        {
            set
            {
                _blocks = value;
            }
            get
            {
                return _blocks;
            }
        }

        public int LenBlock
        {
            set
            {
                _length = value;
            }
            get
            {
                return _length;
            }
        }

        public List<string> ReadFile(bool base64 = false)
        {
            try
            {
                FileStream fileStream = new FileStream(_path, FileMode.Open);
                StreamReader streamReader = new StreamReader(fileStream);
                char[] block = null;

                while (streamReader.Peek() >= 0)
                {
                    if (base64)
                    {
                        block = new char[_length];
                        streamReader.Read(block, 0, block.Length);
                        var textBytes = System.Text.Encoding.UTF8.GetBytes(block); // unicode ??
                        var encodedBase64 = Convert.ToBase64String(textBytes);
                        _blocks.Add(encodedBase64);
                    }
                    else
                    {
                        block = new char[_length]; // * 2
                        streamReader.Read(block, 0, block.Length);
                        _blocks.Add(new string(block));
                    }
                }

                //_blocks[_blocks.Count - 1] = _blocks[_blocks.Count - 1].Replace('\0', 'a');

                fileStream.Dispose();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return _blocks;
        }
    }
}
