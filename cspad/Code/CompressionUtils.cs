using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO.Compression;
using System.IO;
using System.Text;

namespace cspad.web
{
    public static class CompressionUtils
    {
        public static byte[] Compress(string text)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(text);
            return Ionic.Zlib.DeflateStream.CompressBuffer(bytes);
        }

        public static string Decompress(byte[] bytes)
        {
            byte[] decompressed = Ionic.Zlib.DeflateStream.UncompressBuffer(bytes);
            return Encoding.UTF8.GetString(decompressed);
        }
    }
}
