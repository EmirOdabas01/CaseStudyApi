using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudyApi.BusinessLogic.ViewModels.Product
{
    public class UpdateProductVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Stock { get; set; }
        public float Weight { get; set; }
        public float PopularityScore { get; set; }
    }
}
