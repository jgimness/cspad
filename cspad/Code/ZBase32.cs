using System.Collections.Generic;
using System.Text;
namespace cspad.web
{
    /// <summary>
    /// http://en.wikipedia.org/wiki/Base32#z-base-32
    /// </summary>
    public static class ZBase32
    {
        public const string acceptedChar = "ybndrfg8ejkmcpqxot1uwisza345h769";
        public static int encodingLength = acceptedChar.Length; // 32
        private static Dictionary<char, char> aliases;
        static ZBase32()
        {
            aliases = new Dictionary<char, char>();
            aliases.Add('l', '1');
            aliases.Add('0', 'o');

                
        }
        public static string Encode(long input)
        {

            StringBuilder sb = new StringBuilder();

            do
            {
                sb.Append(acceptedChar[(int)(input % encodingLength)]);
                input /= encodingLength;
            } while (input > 0);

            return sb.ToString();
        }

        //public static string Encode(string input)
        //{
        //    StringBuilder sb = new StringBuilder();

        //    do
        //    {
        //        sb.Append(acceptedChar[(int)(input % encodingLength)]);
        //        input /= encodingLength;
        //    } while (input > 0);

        //    return sb.ToString();
        //}

        public static int Decode(string input)
        {
            int decoded = 0;
            for (int i = input.Length; i > 0; i--)
            {
                decoded *= encodingLength;
                decoded += acceptedChar.IndexOf(input[i-1]);
            }
            return decoded;
        }
    }
}
