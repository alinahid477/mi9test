using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mi9.Lib.Models
{
    public class ResponseDTO
    {
        public List<ResponseObject> Response { get; set; }

        public ResponseDTO()
        {
            Response = new List<ResponseObject>();
        }

        public class ResponseObject
        {
            public string Image { get; set; }
            public string Slug { get; set; }
            public string Title { get; set; }
        }
    }
}
