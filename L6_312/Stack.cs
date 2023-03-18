using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;


namespace L6_312
{
    [Serializable]
    public struct Stack
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1000)]
        Record[] records;
        int count;
        int size;
        /*const int defLength = 10;
        public Stack()
        {
            records = new Record[defLength];
        }*/
        public Stack(int length)
        {
            count = 0;
            size = length;
            records = new Record[length];
        }
        public bool isEmpty
        {
            get{ return count == 0; } 
        }
        public int Count
        {
            get{ return count; }
        }

        public int Size
        {
            get{ return size; }
        }
        public bool Push(Record record)
        {
            if(count == records.Length)
            {
                return false;
            }
            records[count++] = record;
            return true;
        }


    }
}
