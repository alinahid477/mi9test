using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mi9.Lib.Services
{
    public class IOService
    {
        public string Read(System.IO.StreamReader reader)
        {
            string json = reader.ReadToEnd();
            reader.Dispose();
            return json;
        }

        public string Read(string filePath)
        {
            System.IO.StreamReader reader = new System.IO.StreamReader(filePath);
            return this.Read(reader);
        }
    }
}
