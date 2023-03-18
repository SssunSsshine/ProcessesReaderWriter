using System;
using System.IO.MemoryMappedFiles;
using L6_312;
using System.Threading;

namespace L6_312_Reader
{
    class Program
    {
        static public Mutex mutex = new Mutex(false, "GlobalMutex");
        static void Main(string[] args)
        {

            MemoryMappedFile sharedMemory;
            int sizeInt = sizeof(int);
            int count;
            int sizeStack;
            while (true)
            {
                if (!mutex.WaitOne(TimeSpan.FromSeconds(5), false))
                {
                    continue;
                }
                try
                {
                    sharedMemory = MemoryMappedFile.OpenExisting("MemoryFile");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    continue;
                }
                Record record = new Record(0, 0);
                int posRec;
                using (MemoryMappedViewAccessor reader = sharedMemory.CreateViewAccessor(0, 0, MemoryMappedFileAccess.Read))
                {
                    sizeStack = reader.ReadInt32(0);
                    count = reader.ReadInt32(sizeInt);
                    if (count != 0)
                    {
                        posRec = sizeInt * 2 + (count - 1) * sizeInt * 2;
                        record.processId = reader.ReadInt32(posRec);
                        record.numRecord = reader.ReadInt32(posRec+sizeInt);
                        /*reader.Read<Record>(posRec, out record);*/
                    }
                }
                if (count != 0)
                {
                    using (MemoryMappedViewAccessor writer = sharedMemory.CreateViewAccessor(sizeInt, 0))
                    {
                        writer.Write(0, count - 1);
                    }

                    Console.WriteLine("Запись стерта: ");
                    Console.WriteLine(record);
                }
                mutex.ReleaseMutex();
            }
            
        }
    }
}
