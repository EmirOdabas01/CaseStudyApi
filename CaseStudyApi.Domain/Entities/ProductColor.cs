using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudyApi.Domain.Entities
{
    public class ProductColor
    {
        public int Id { get; set; }
        public string Color { get; set; }
        public ICollection<ProductImageFile> ProductImageFiles { get; set; }
    }
}
