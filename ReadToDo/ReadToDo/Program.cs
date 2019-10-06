using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ReadToDo
{
    public class Program
    {
        const string TODO = "TODO";

        public static void Main(string[] args)
        {
            ReadFilePath();
        }

        private static void ReadFilePath()
        {
            try
            {
                Console.Write("Enter your directory :");
                string fileDirectory = Console.ReadLine();
                List<string> files = GetFiles(fileDirectory);
                PrintToDoFiles(files);

                while (true)
                {
                    Console.Write("Do you want to continue? 1 - Yes, 2 - No : ");
                    string option = Console.ReadLine();
                    if (!option.Equals("1") && !option.Equals("2"))
                    {
                        Console.WriteLine("Invalid option! Please try again!");
                        continue;
                    }
                    else if (option.Equals("1"))
                    {
                        ReadFilePath();
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Good bye!");
                        break;
                    }

                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Please try again!");
                ReadFilePath();
            }
        }

        private static void PrintToDoFiles(List<string> files)
        {
            foreach (string file in files)
            {
                Console.WriteLine(file);
            }
        }

        public static List<string> GetFiles(string fileDirectory)
        {
            List<string> todoFiles = new List<string>();

            try
            {
                string[] files = Directory.GetFiles(fileDirectory, "*", SearchOption.AllDirectories);

                foreach (string file in files)
                {
                    using (FileStream fileStream = File.OpenRead(file))
                    {
                        byte[] fileContents = new byte[fileStream.Length];
                        UTF8Encoding decoding = new UTF8Encoding(true);

                        while (fileStream.Read(fileContents, 0, fileContents.Length) > 0)
                        {
                            string content = decoding.GetString(fileContents);

                            if (content.ToUpperInvariant().Contains(TODO))
                            {
                                todoFiles.Add(file);
                                break;
                            }
                        }
                    }
                }
            }
            catch (ArgumentException ex)
            {
                throw ex;
            }
            catch (UnauthorizedAccessException ex)
            {
                throw ex;
            }
            catch (PathTooLongException ex)
            {
                throw ex;
            }
            catch (IOException ex)
            {
                throw ex;
            }

            return todoFiles;

        }
    }
}
