using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;

namespace PossiblePawnsMoves
{
    public static class FileStreamer
    {
        public static IEnumerable<string> ReadFile(string path)
        {
            var rawInput = new Collection<string>();

            try
            {
                foreach (var file in Directory.GetFiles(path, "*.txt"))
                {
                    using (StreamReader streamReader = new StreamReader(file))
                    {
                        while (streamReader.Peek() >= 0)
                        {
                            rawInput.Add(streamReader.ReadLine());
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Oops... :C");
                Console.WriteLine($"Type of error: {e.GetType()}");
                Console.WriteLine(e.Message);
            }

            return rawInput;
        }

        public static void WriteFile(Dictionary<string, string> output, string path)
        {
            try
            {
                using (StreamWriter streamWriter = new StreamWriter(Path.Combine(path, "output.txt")))
                {
                    foreach (var item in output)
                    {
                        streamWriter.WriteLine(item.Key);
                        streamWriter.Write(item.Value);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Oops... :C");
                Console.WriteLine($"Type of error: {e.GetType()}");
                Console.WriteLine(e.Message);
            }
        }
    }
}