using System;
using System.Collections.Generic;
using System.Text;

namespace Toptin.Api.Models
{
    public class Picture
    {
        public int PictureId { get; set; }
        public byte[] Pic { get; set; }
        public string Title { get; set; }
        public int Type { get; set; }
    }
}
