/**************************************************************************
 *                                                                        *
 *  File:        Program.cs                                               *
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

using System;
using DryIoc;
using Security.Interfaces;
using Security.Implementations;
using Security.Utils.IoC;
using System.Collections.Generic;
using Security.Utils.FileHelper;
using Security.CustomException;
using System.Diagnostics;
using System.Text;

namespace Security
{
    class Program
    {
        static void Main(string[] args)
        {
            var order = new Algorithms[] {
                Algorithms.HillChiper,
                Algorithms.RailFence,
                Algorithms.BookChiper,
            };

            try
            {
                var bootstrapper = new Bootstrap(order);
                var algorithms = bootstrapper.Container.Resolve<IList<ICrypto>>();

                var cipher = new ComplexCipher(algorithms);
                cipher.Crypt();
                cipher.Decrypt();
            }
            catch (SecurityPositiveNumber ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (SecurityEvenNumber ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}
