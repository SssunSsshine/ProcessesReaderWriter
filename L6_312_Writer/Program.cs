using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.MemoryMappedFiles;
using L6_312;
using System.Threading;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace L6_312_Writer
{
    class Program
    {
        static public Mutex mutex = new Mutex(false, "GlobalMutex");
        static void Main(string[] args)
        {
            int count = 0;
            int size;
            int sizeInt = sizeof(int);
            int num = Int32.Parse(args[0]);
            while (true)
            {
                
                if (!mutex.WaitOne(TimeSpan.FromSeconds(5), false))
                {
                    continue;
                }
                MemoryMappedFile sharedMemory;
                try
                {
                    sharedMemory = MemoryMappedFile.OpenExisting("MemoryFile");
                }
                catch 
                {
                    return;
                }
                
                using (MemoryMappedViewAccessor reader = sharedMemory.CreateViewAccessor(0, 2*sizeInt, MemoryMappedFileAccess.Read))
                {
                    size = reader.ReadInt32(0);
                    count = reader.ReadInt32(4);
                }
                Process process = Process.GetCurrentProcess();
                Process pr = Process.GetProcessById(process.Id);
                if (count == size)
                {
                    mutex.ReleaseMutex();
                    pr.Kill();
                }
                
                Record rec = new Record(process.Id,num++);
                count++;
                int posProcessId = sizeInt + sizeInt * 2 * (count - 1);
                using (MemoryMappedViewAccessor writer = sharedMemory.CreateViewAccessor(sizeInt, 0))
                {
                    writer.Write(0, count);
                    writer.Write(posProcessId,rec.processId);
                    writer.Write(posProcessId + sizeInt, rec.numRecord);
                    /*writer.Write<Record>(posProcessId,ref rec);*/
                }
                Console.WriteLine("Сообщение записано в общую память");
                Console.WriteLine(rec);
                mutex.ReleaseMutex();
            }
        }
    }
}
