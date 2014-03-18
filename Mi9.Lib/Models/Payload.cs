using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mi9.Lib.Models
{
    public class Payload
    {
        public bool Drm{get;set;}
        public int EpisodeCount { get; set; }
        public ImageClass Image { get; set; }
        public string Slug { get; set; }
        public string Title { get; set; }

        public class ImageClass
        {
            public string ShowImage { get; set; }
        }
    }
}
