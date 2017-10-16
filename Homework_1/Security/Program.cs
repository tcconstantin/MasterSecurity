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
