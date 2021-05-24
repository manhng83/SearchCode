using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Diagnostics;

namespace ConsoleApplication2
{
    class Program
    {
        static void Main(string[] args)
        {
            var sb = new StringBuilder();
            var lst = new List<string>();
            string folder = @"C:\Users\manhn\source\repos\";
            string[] files = Directory.GetFiles(folder, "*.cs", SearchOption.AllDirectories);
            foreach (var file in files)
            {
                try
                {
                    string[] lines = File.ReadAllLines(file, Encoding.UTF8);
                    foreach (var line in lines)
                    {
                        try
                        {
                            if (
                                //Filter by specify text in code line
                                line.Contains("using System.Reflection;")
                                //Filter by file name
                                && !file.Contains("Microsoft")
                                && !file.EndsWith("AssemblyInfo.cs")
                                //Filter by folder name
                                && !file.Contains(@"\Release\")
                                && !file.Contains(@"\Debug\")
                                && !file.Contains(@"\obj\")
                                && !file.Contains(@"\bin\")
                                )
                            {
                                sb.AppendLine(file);
                                lst.Add(file);
                                break;
                            }
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine(ex.ToString());
                            //throw;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.ToString());
                    //throw;
                }
            }

            string TextNotepadEditor = @"C:\Windows\System32\notepad.exe";
            string TextNotepadPlusPlusEditor = @"C:\Program Files\Notepad++\notepad++.exe";

            if (File.Exists(TextNotepadPlusPlusEditor))
            {
                TextNotepadEditor = TextNotepadPlusPlusEditor;
            }

            File.WriteAllText("AllFiles.txt", sb.ToString(), Encoding.UTF8);
            Process.Start(TextNotepadEditor, "AllFiles.txt");

            foreach (var file in lst)
            {
                Process.Start(TextNotepadEditor, file);
            }

            Console.WriteLine("DONE !!! Press any key to exit . . .");
            Console.ReadKey();
        }
    }
}
