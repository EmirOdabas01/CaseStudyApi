using CaseStudyApi.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudyApi.Domain.Entities
{
    public class Product : BaseEntity
    {
        public int Stock { get; set; }
        public float Weight { get; set; }
        public float PopularityScore { get; set; }
        public ICollection<ProductImage> Images { get; set; }
    }
}
