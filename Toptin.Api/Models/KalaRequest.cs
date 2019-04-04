using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Toptin.Api.Models
{
    public class KalaRequest
    {
        [Key]
        public int KalaRequestId { get; set; }
        public string Title { get; set; }
        public string Desc { get; set; }
        public int Status { get; set; }
        public int Num { get; set; }
        public int Price { get; set; }

        public string Id { get; set; }
        public User User { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int BrandId { get; set; }
        public Brand Brand { get; set; }
    }
}
