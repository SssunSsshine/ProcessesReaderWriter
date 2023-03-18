using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.MemoryMappedFiles;
using System.Diagnostics;
using System.Threading;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace L6_312
{
    class Program
    {
        static public Mutex mutex = new Mutex(false, "GlobalMutex");
        const int stackSize = 1000;
        static void Main(string[] args)
        {
            MemoryMappedFile sharedMemory = MemoryMappedFile.CreateOrOpen("MemoryFile", stackSize*2*sizeof(int)+2*sizeof(int));
            using (MemoryMappedViewAccessor memoryMappedViewStream = sharedMemory.CreateViewAccessor())
            {
                memoryMappedViewStream.Write(0,stackSize);
                memoryMappedViewStream.Write(4, 0);
            }
             int i = 0;
             Process reader = new Process();
             reader.StartInfo.FileName = "D:\\User\\unic\\Software\\L6_312\\L6_312_Reader\\bin\\Debug\\L6_312_Reader.exe";
             reader.Start();
             while (i<4)
             {
                Process writer = new Process();
                writer.StartInfo.FileName = "D:\\User\\unic\\Software\\L6_312\\L6_312_Writer\\bin\\Debug\\L6_312_Writer.exe";
                writer.StartInfo.Arguments = "0";
                i++;
                writer.Start();
            }
             Console.ReadLine();
        }
    }
}
