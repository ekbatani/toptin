using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Toptin.Api.Models
{
    public class Kala
    {
        [Key]
        public int KalaId { get; set; }
        public string Title { get; set; }
        public string TitleEn { get; set; }
        public string Garantee { get; set; }
        public string DescShort { get; set; }
        public string Desc { get; set; }
        public int Star { get; set; }
        public int Hit { get; set; }
        public int StarNum { get; set; }
        public int Type { get; set; }

        public string Id { get; set; }
        public User User { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int BrandId { get; set; }
        public Brand Brand { get; set; }
    }
}
