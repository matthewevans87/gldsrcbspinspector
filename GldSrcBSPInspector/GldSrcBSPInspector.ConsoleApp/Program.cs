//using GldSrcBSPInspector.ConsoleApp.BSPFormat;
using System;
using System.IO;
using System.Linq;
using CsvHelper;
using System.Collections.Generic;
using GldSrcBSPInspector.ConsoleApp.Structs;
using System.Runtime.InteropServices;

namespace GldSrcBSPInspector.ConsoleApp
{
    //http://hlbsp.sourceforge.net/index.php?content=bspdef

    class Program
    {
        static void Main(string[] args)
        {
            var workingDir = args[0];
            //listWadsLegacy(workingDir);

            var files = Directory.GetFiles(workingDir, "*.bsp");
            if (!files.Any())
            {
                Console.WriteLine("No bsp files found in working this directory.");
                Console.WriteLine("Try providing the path of your maps directory as a command line parameter.");
            }
            foreach (var filePath in files)
            {
                listWadsSpan(filePath);
            }

            Console.ReadLine();
        }

        private static void listWadsSpan(string filePath)
        {
            var bspfs = new BSPFileSpan();
            bspfs.ReadFile(filePath);
        }

        //private static void listWadsLegacy(string workingDir)
        //{
        //    var files = Directory.GetFiles(workingDir, "*.bsp");
        //    if (!files.Any())
        //    {
        //        Console.WriteLine("No bsp files found in working this directory.");
        //        Console.WriteLine("Try providing the path of your maps directory as a command line parameter.");
        //    }


        //    var wadcsvs = new List<WADCSV>();
        //    foreach (var path in files)
        //    {
        //        try
        //        {
        //            Console.WriteLine(Path.GetFileName(path));
        //            var bsp = new BSPFile(path);
        //            if (!bsp.WADs.Any())
        //            {
        //                Console.WriteLine("No wads listed.");
        //            }
        //            else
        //            {
        //                foreach (var wad in bsp.WADs)
        //                {
        //                    Console.WriteLine(wad);
        //                    wadcsvs.Add(new WADCSV()
        //                    {
        //                        BSPPath = path,
        //                        WADPath = wad
        //                    });
        //                }
        //            }
        //        }
        //        catch (Exception e)
        //        {
        //            Console.WriteLine(e.Message);
        //        }
        //        Console.WriteLine();
        //    }

        //    var outputFileName = DateTime.Now.Ticks.ToString() + ".csv";
        //    using (var fs = new FileStream(outputFileName, FileMode.CreateNew))
        //    {
        //        using (var tw = new StreamWriter(fs))
        //        {
        //            var csv = new CsvWriter(tw);
        //            csv.WriteHeader<WADCSV>();
        //            csv.NextRecord();
        //            csv.WriteRecords(wadcsvs);
        //        }
        //    }
        //}
    }

    public class WADCSV
    {
        public string BSPPath { get; set; }
        public string WADPath { get; set; }
    }

}
