using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BaseEncodingTest
{
    class Program
    {
        static void Main(string[] args)
        {
            for(int i=0; i < 1000000; i++)
            {
                string encoded = ZBase32.Encode(i);
                Console.WriteLine("{0} => {1}", i, encoded);

                if(i != ZBase32.Decode(encoded))
                {
                    throw new InternalBufferOverflowException();
                }
            }
            
            Console.ReadLine();
        }
    }
}
