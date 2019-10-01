using System;
using System.IO;
using System.Text;

namespace ReadToDo
{
    class Program
    {
        const string TODO = "TODO";

        static void Main(string[] args)
        {
            Console.Write("Enter your directory:");
            string fileDirectory = Console.ReadLine();
            GetFiles(fileDirectory);
        }

        private static void GetFiles(string fileDirectory)
        {
            string[] files = Directory.GetFiles(fileDirectory, "*.js", SearchOption.AllDirectories);

            foreach (string file in files)
            {
                using (FileStream fileStream = File.OpenRead(file))
                {
                    byte[] fileContents = new byte[fileStream.Length];
                    UTF8Encoding decoding = new UTF8Encoding(true);

                    while (fileStream.Read(fileContents, 0, fileContents.Length) > 0)
                    {
                        string content = decoding.GetString(fileContents);

                        if (content.Contains(TODO))
                        {
                            Console.WriteLine(file);
                            break;
                        }
                    }
                }
            }
        }
    }
}
