using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LZW
{
    internal class Program
    {
        static void Main(string[] args)
        {
            LZW lzw = new LZW();
            lzw.CompressFile("data.txt", "arch_data.bin");
            lzw.DecompressFile("arch_data.bin", "dearch_data.txt");
        }
    }
}
