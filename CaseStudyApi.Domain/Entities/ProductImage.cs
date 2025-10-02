using CaseStudyApi.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudyApi.Domain.Entities
{
    public class ProductImage : BaseEntity
    {
        public string Path { get; set; }
        [NotMapped]
        public override DateTime UpdatedDate { get => base.UpdatedDate; set => base.UpdatedDate = value; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
