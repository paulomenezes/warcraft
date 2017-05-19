using System;
using System.IO;

namespace Warcraft.Util
{
    public class Data
    {
        public static void Write(String text) 
        {
            using (StreamWriter sw = File.AppendText(@"../../data.txt"))
            {
                sw.WriteLine(text);
            }
		}
    }
}
