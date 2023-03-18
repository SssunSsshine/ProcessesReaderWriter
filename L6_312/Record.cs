using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;

namespace L6_312
{
    [Serializable]
    public struct Record
    {
        public int processId;
        public int numRecord;
        public Record(int processId, int numRecord)
        {
            this.processId = processId;
            this.numRecord = numRecord;
        }
        public override string ToString()
        {
            return "{Process name = " + processId +
                    ", Record number = " + numRecord + "}";
        }
    }
}
