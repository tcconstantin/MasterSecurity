using Security.CustomException;
using Security.Implementations.Options;
using Security.Interfaces;
using Security.Utils.FileHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Security.Implementations
{
    public class HillCipher : ICrypto
    {
        private Random _random;
        private Reader _reader;
        private HillCipherOption _option;
        private int[,] _involutoryMatrix;
        private const char _firstChar = 'A';

        private Dictionary<int, char> _base64Table;

        public HillCipher(HillCipherOption option)
        {
            this._reader = new Reader(option.PathInputFile, option.LenBlock);
            this._random = new Random();
            this._involutoryMatrix = new int[option.LenBlock * 2, option.LenBlock * 2];
            this._option = option;
            this._base64Table = new Dictionary<int, char>();

            if (File.Exists(_option.PathCryptoFile)) File.Delete(_option.PathCryptoFile);
            if (File.Exists(_option.PathDecryptFile)) File.Delete(_option.PathDecryptFile);

            GenerateBase64Table();
            GenerateMatrix();
        }

        private void GenerateBase64Table()
        {
            for (int i = 'A'; i <= 'Z'; ++i) _base64Table.Add((char)i - 'A', (char)i);
            for (int i = 'a'; i <= 'z'; ++i) _base64Table.Add((char)i - 'A' - 6, (char)i);
            for (int i = '0'; i <= '9'; ++i) _base64Table.Add((char)i + 4, (char)i);

            _base64Table.Add('+' + 19, '+');
            _base64Table.Add('/' + 16, '/');
            _base64Table.Add('=' + 3, '=');
        }

        private void GenerateMatrix()
        {
            var value = 0;
            int i = 0, j = 0;
            _option.LenBlock *= 2;
            // Generate A22 block
            for (i = _option.LenBlock / 2; i < _option.LenBlock; ++i)
            {
                for (j = _option.LenBlock / 2; j < _option.LenBlock; ++j)
                {
                    this._involutoryMatrix[i, j] = _random.Next(_option.Mod - 2) + 1;
                }
            }
            // Calculate A11 block
            for (i = 0; i < _option.LenBlock / 2; ++i)
            {
                for (j = 0; j < _option.LenBlock / 2; ++j)
                {
                    this._involutoryMatrix[i, j] = _option.Mod -
                        (this._involutoryMatrix[i + _option.LenBlock / 2, j + _option.LenBlock / 2] % _option.Mod);
                }
            }
            // Calculate A12 block
            for (i = 0; i < _option.LenBlock / 2; ++i)
            {
                for (j = _option.LenBlock / 2; j < _option.LenBlock; ++j)
                {
                    if (j - i == _option.LenBlock / 2)
                    {
                        value = 1 - this._involutoryMatrix[i, j - _option.LenBlock / 2];
                    }
                    else
                    {
                        value = (-1) * this._involutoryMatrix[i, j - _option.LenBlock / 2];
                    }

                    this._involutoryMatrix[i, j] = (value < 0 ? _option.Mod - ((value * (-1)) % _option.Mod) : value % _option.Mod);
                }
            }
            // Calculate A21 block
            for (i = _option.LenBlock / 2; i < _option.LenBlock; ++i)
            {
                for (j = 0; j < _option.LenBlock / 2; ++j)
                {
                    if (i - j == _option.LenBlock / 2)
                    {
                        value = 1 + this._involutoryMatrix[i - _option.LenBlock / 2, j];
                    }
                    else
                    {
                        value = this._involutoryMatrix[i - _option.LenBlock / 2, j];
                    }
                    this._involutoryMatrix[i, j] = (value < 0 ? _option.Mod - ((value * (-1)) % _option.Mod) : value % _option.Mod);
                }
            }
            _option.LenBlock /= 2;
        }

        private string CryptBlock(string text)
        {
            var cryptText = "";
            int value = 0;
            _option.LenBlock *= 2;
            for (int i = 0; i < _option.LenBlock; i++)
            {
                value = 0;
                for (int j = 0; j < _option.LenBlock; j++)
                {
                    var key = _base64Table.FirstOrDefault(x => x.Value == (char)text[j]).Key;
                    value += this._involutoryMatrix[i, j] * key;
                }

                char currentChar;
                _base64Table.TryGetValue((char)(value % _option.Mod), out currentChar);
                cryptText += currentChar;
            }
            _option.LenBlock /= 2;
            return cryptText;
        }

        private void Logic(string outputFile, bool readBase64 = false, bool writeBase64 = false)
        {
            _reader.Blocks = new List<string>();

            var text = _reader.ReadFile(readBase64);
            var currentBlock = "";

            using (StreamWriter streamWriter = File.AppendText(outputFile))
            {
                for (int i = 0; i < text.Count; ++i)
                {
                    currentBlock = CryptBlock(text[i]);
                    if (writeBase64)
                    {
                        var base64EncodedBytes = System.Convert.FromBase64String(currentBlock);
                        streamWriter.Write(System.Text.Encoding.UTF8.GetString(base64EncodedBytes));
                    }
                    else
                    {
                        streamWriter.Write(currentBlock);
                    }
                }
            }
        }

        public void Crypt()
        {
            Logic(_option.PathCryptoFile, true);
        }

        public void Decrypt()
        {
            _reader.Path = _option.PathCryptRailFence;
            _reader.LenBlock *= 2;
            Logic(_option.PathDecryptFile, false, true);
        }
    }
}
