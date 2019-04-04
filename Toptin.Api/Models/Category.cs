using System;
using System.Collections.Generic;
using System.Text;

namespace Toptin.Api.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Title { get; set; }
        public bool Root { get; set; }

        public int? ParentCategoryId { get; set; }
        public Category ParentCategory { get; set; }
    }
}
