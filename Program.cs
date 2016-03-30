using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Globalization;

namespace VectorDrawableScientificConverter
{
    class Program
    {
        public static string regex = @"[+|-]?\d\.?\d{0,}[E|e|X|x](10)?[\^\*]?[+|-]?\d+";
        //max decimal string const
        //can be done programatically with "0." + new string('#', 339)
        public static string decimalFormat = "0.###################################################################################################################################################################################################################################################################################################################################################";
        static void Main(string[] args)
        {
            bool running = true;
            while (running)
            {
                try
                {
                    Console.WriteLine("Enter a file path for conversion: ");
                    string pathString = Console.ReadLine();
                    //pathString = @"C:\Users\timot\Documents\GitHub\VoterApp\app\src\main\res\drawable\splash_south_carolina.xml";
                    string[] contents = File.ReadAllLines(pathString);
                    int converted = 0;
                    for (int i = 0; i < contents.Length; i++)
                        for (Match m = Regex.Match(contents[i], regex); !string.IsNullOrWhiteSpace(m.Value); m = Regex.Match(contents[i], regex))
                        {
                            decimal d = decimal.Parse(m.ToString(), NumberStyles.Float | NumberStyles.AllowExponent | NumberStyles.AllowDecimalPoint);
                            string dstring = d.ToString(decimalFormat);
                            string line = contents[i];
                            line = line.Remove(m.Index, m.Length);
                            line = line.Insert(m.Index, dstring);
                            contents[i] = line;
                            converted++;
                        }
                    File.Delete(pathString);
                    File.WriteAllLines(pathString, contents);
                    Console.WriteLine("Converted {0} instances of scientific notation to decimal.", converted);
                    Console.ReadLine();
                    running = false;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }
    }
}
