using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudyApi.BusinessLogic.Dtos.ProductImageFile
{
    public class ProductImageFileReadDto
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public string Color { get; set; }
        public bool IsShowCase { get; set; }
        public int Id { get; set; }
    }
}
