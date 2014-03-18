using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mi9.Lib.Models
{
    public class BaseModel
    {
        public bool IsValid
        {
            get;
            set;
        }
        public virtual void Validate()
        {
            this.IsValid = true;
        }
    }
}
