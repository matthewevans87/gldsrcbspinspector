using GldSrcBSPInspector.ConsoleApp.Structs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace GldSrcBSPInspector.ConsoleApp
{
    public class BSPFileSpan
    {

        public void ReadFile(string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.Open))
            {
                var itemSize = Marshal.SizeOf<Header>();

                Span<Header> buffer = new Header[15]; // alloc items buffer
                var rawBuffer = MemoryMarshal.Cast<Header, byte>(buffer); // cast items buffer to bytes buffer (no copies)

                var bytesRead = fs.Read(rawBuffer);
                while (bytesRead > 0)
                {
                    var itemsRead = bytesRead / itemSize;
                    foreach (var foo in buffer.Slice(0, itemsRead)) // iterate through the item buffer
                        Console.WriteLine(foo);
                    bytesRead = fs.Read(rawBuffer);
                }
                /*
                 * Close; but its not an array of Headers, rather 1 header with an array of Lumps
                 */
            }
        }

    }
}

namespace GldSrcBSPInspector.ConsoleApp.Structs
{
    public struct Header
    {
        public Int32 Version;
        public Lump Lumps;
    }

    public struct Lump
    {
        public Int32 Offset;
        public Int32 Length;
    }
}
