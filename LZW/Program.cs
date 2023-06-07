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
            lzw.CompressFile(@"D:\Третий курс\ТОИ\Лабораторная работа №5\LZW\LZW\bin\Debug\net6.0\data.txt", @"D:\Третий курс\ТОИ\Лабораторная работа №5\LZW\LZW\bin\Debug\net6.0\arch_data.bin");
            lzw.DecompressFile(@"D:\Третий курс\ТОИ\Лабораторная работа №5\LZW\LZW\bin\Debug\net6.0\arch_data.bin", @"D:\Третий курс\ТОИ\Лабораторная работа №5\LZW\LZW\bin\Debug\net6.0\dearch_data.txt");
        }
    }
}
