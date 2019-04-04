using System;
using System.Collections.Generic;
using System.Text;

namespace Toptin.Api.Models
{
    public class KalaPicture
    {
        public int KalaPictureId { get; set; }

        public int KalaId { get; set; }
        public Kala Kala { get; set; }
        public int PictureId { get; set; }
        public Picture Picture { get; set; }
    }
}
