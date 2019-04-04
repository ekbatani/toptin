using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Toptin.Api.Models
{
    public class Brand
    {
        [Required]
        public int BrandId { get; set; }
        public string Title { get; set; }
    }
}
