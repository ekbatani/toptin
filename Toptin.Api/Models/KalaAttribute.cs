using System;
using System.Collections.Generic;
using System.Text;

namespace Toptin.Api.Models
{
    public class KalaAttribute
    {
        public int KalaAttributeId { get; set; }
        public string Title { get; set; }
        public string Value { get; set; }

        public int KalaId { get; set; }
        public Kala Kala { get; set; }
        public int AttributeCategoryId { get; set; }
        public AttributeCategory AttributeCategory { get; set; }
    }
}
