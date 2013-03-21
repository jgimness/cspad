using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cspad.util
{
    public class ZBase32
    {
        public const string acceptedChar = "ybndrfg8ejkmcpqxot1uwisza345h769";
        public static int char_length = acceptedChar.Length;

        public static string Encode(int input)
        {
            string encoded = "";

            do
            {
                encoded += acceptedChar[input%acceptedChar.Length];
                input /= acceptedChar.Length;
            } while (input > 0);

            return encoded;
        }

        public static int Decode(string input)
        {
            int decoded = 0;
            for (int i = input.Length; i > 0; i--)
            {
                decoded *= char_length;
                decoded += acceptedChar.IndexOf(input[i-1]);
            }
            return decoded;
        }
    }
}
