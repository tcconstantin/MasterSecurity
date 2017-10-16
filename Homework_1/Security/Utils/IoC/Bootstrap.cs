using System;
using System.Collections.Generic;
using System.Text;
using DryIoc;
using Security.Interfaces;
using Security.Implementations;
using Security.Implementations.Options;

namespace Security.Utils.IoC
{
    public class Bootstrap
    {
        private Container _container;

        public Bootstrap(Algorithms[] order)
        {
            this._container = new Container();

            foreach(var currentOrder in order)
            {
                switch(currentOrder)
                {
                    case Algorithms.HillChiper:
                        this._container.Register<ICrypto, HillCipher>();

                        var optionHillCipher = new HillCipherOption
                        {
                            Mod = 65,
                            LenBlock = 4,
                            PathInputFile = @"Resource\test.txt",
                            PathCryptoFile = @"Resource\hillcipher_crypt.txt",
                            PathDecryptFile = @"Resource\hillcipher_decrypt.txt",
                            PathCryptRailFence = @"Resource\railfence_decrypt.txt"
                        };

                        this._container.RegisterInstance(optionHillCipher);
                        break;
                    case Algorithms.RailFence:
                        this._container.Register<ICrypto, RailFence>();

                        var optionRailFence = new RailFenceOption
                        {
                            LenBlock = 4,
                            LenKey = 4,
                            PathInputFile = @"Resource\hillcipher_crypt.txt",
                            PathCryptoFile = @"Resource\railfence_crypt.txt",
                            PathDecryptFile = @"Resource\railfence_decrypt.txt",
                            PathCryptBookCipher = @"Resource\bookcipher_decrypt.txt"
                        };

                        this._container.RegisterInstance(optionRailFence);
                        break;
                    case Algorithms.BookChiper:
                        this._container.Register<ICrypto, BookCipher>();

                        var optionBookCipher = new BookCipherOption
                        {
                            LenBlock = 8,
                            MaxNumber = 999999,
                            PathInputFile = @"Resource\railfence_crypt.txt",
                            PathCryptoFile = @"Resource\bookcipher_crypt.txt",
                            PathDecryptFile = @"Resource\bookcipher_decrypt.txt"
                        };

                        this._container.RegisterInstance(optionBookCipher);
                        break;
                }
            }           
        }

        public Container Container
        {
            get
            {
                return this._container;
            }
        }
    }
}
