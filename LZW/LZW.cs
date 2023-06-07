using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LZW
{
    internal class LZW
    {

        // ------------------------- Compress ---------------------------------
        public void CompressFile(string dataFileName, string archFileName)
        {
            byte[] data = File.ReadAllBytes(dataFileName);
            byte[] arch = Compress(data);
            File.WriteAllBytes(archFileName, arch);
        }

        private byte[] Compress(byte[] data)
        {
            List<byte> arch = new List<byte>();
            List<byte> bytes = new List<byte>();

            Dictionary<string, int> dictionary = new Dictionary<string, int>();
            int code_in_dictionary = 256;
            string symbol_pairs = "";
            for (int i = 0; i < data.Length; ++i)
            {
                symbol_pairs = data[i].ToString();
                for (int j = i + 1; j < data.Length; ++j)
                {
                    string temp = symbol_pairs;
                    symbol_pairs += data[j].ToString();
                    if (!dictionary.ContainsKey(symbol_pairs))
                    {
                        if(code_in_dictionary <= 255*256 + 255)
                            dictionary.Add(symbol_pairs, code_in_dictionary++);
                        if (i == j - 1)
                        {
                            arch.Add((byte)(0 & 255));
                            arch.Add((byte)(data[i] & 255));
                            arch.Add((byte)(data[j] & 255));
                        }
                        else
                        {
                            int n = dictionary[temp] / 256;
                            arch.Add((byte)(n & 255));
                            arch.Add((byte)((dictionary[temp] - n*256) & 255));
                            arch.Add(data[j]);
                        }
                        i = j;
                        break;
                    }
                    if (j + 1 >= data.Length)
                    {
                        if(bytes.Count % 3 == 0)
                        {
                            bytes.Add((byte)0);
                            bytes.Add((byte)data[i]);
                        }
                        else if(bytes.Count % 2 == 0)
                        {
                            bytes.Add(data[i]);
                            arch = arch.Concat(bytes).ToList();
                            bytes = new List<byte>();
                        }
                    }
                }
            }
            return arch.ToArray();
        }

        // ------------------------- Decompress ---------------------------------
        public void DecompressFile(string archFileName, string dataFileName)
        {
            byte[] arch = File.ReadAllBytes(archFileName);
            byte[] data = Decompress(arch);
            File.WriteAllBytes(dataFileName, data);
        }

        private byte[] Decompress(byte[] arch)
        {
            Dictionary<int, List<byte>> dictionary = new Dictionary<int, List<byte>>();
            List<byte> data = new List<byte>();
            int code_in_dictinoray = 256;
            for (int i = 0; i < arch.Length; i += 3)
            {
                int count = arch[i];
                byte code = arch[i+1];
                byte sym = arch[i+2];
                if (count == 0)
                {
                    data.Add(code);
                    data.Add(sym);
                    if (code_in_dictinoray <= 255 * 256 + 255)
                        dictionary.Add(code_in_dictinoray++, new List<byte>() { code, sym});
                }
                else
                {
                    data = data.Concat(dictionary[count * 256 + code]).ToList();
                    data.Add(sym);
                    List<byte> temp = new List<byte>().Concat(dictionary[count * 256 + code]).ToList();
                    temp.Add(sym);
                    if (code_in_dictinoray <= 255 * 256 + 255)
                        dictionary.Add(code_in_dictinoray++, temp);
                }
            }
            return data.ToArray();
        }
    }
}
