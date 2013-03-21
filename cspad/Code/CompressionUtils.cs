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
            //;
            //byte[] compressed;

            //using (var stream = new MemoryStream())
            //{
            //    using (var deflate = new DeflateStream(stream, CompressionMode.Compress))
            //    {
            //        deflate.Write(bytes, 0, bytes.Length);
            //    }
                
            //    compressed = stream.ToArray();
            //}

            //return compressed;
        }

        public static string Decompress(byte[] bytes)
        {
            byte[] decompressed = Ionic.Zlib.DeflateStream.UncompressBuffer(bytes);
            return Encoding.UTF8.GetString(decompressed);
            //using (var compressed = new MemoryStream(bytes))
            //{
            //    using (var deflate = new DeflateStream(compressed, CompressionMode.Decompress))
            //    {
            //        using(var decompressed = new MemoryStream())
            //        {
                        
            //        }
            //    }

            //    decompressed = stream.ToArray();
            //}

            //return Encoding.UTF8.GetString(decompressed);
        }
    }
}
