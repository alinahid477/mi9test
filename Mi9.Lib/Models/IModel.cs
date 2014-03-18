using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mi9.Lib.Models
{
    public interface IModel
    {
        void Validate();
        void Read(string source);
        string Write();
    }
}
