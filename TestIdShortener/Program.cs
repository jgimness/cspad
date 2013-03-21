using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cspad.web;

namespace TestIdShortener
{
    class Program
    {
        static void Main(string[] args)
        {
            var zBase32 = new ZBase32Encoder();

            byte[] inputData = new byte[] { 0xF0, 0xBF, 0xC7 };
            string encodedText = zBase32.Encode(inputData);

            string input = null;
            do
            {
                input = Console.ReadLine();

                long number;

                if (long.TryParse(input, out number))
                {
                    //byte[] bytes = BitConverter.GetBytes((int)number);

                    string output = ZBase32.Encode(number);
                    Console.WriteLine(output);

                    long decoded = ZBase32.Decode(output); // 4242424242
                    if(decoded != number) {
                        throw new IndexOutOfRangeException();
                    }
                    
                }
                else
                {
                    Console.WriteLine("Not a number, bigger than {0}", long.MaxValue);
                }
            } while (input != null);
        }
    }
}
