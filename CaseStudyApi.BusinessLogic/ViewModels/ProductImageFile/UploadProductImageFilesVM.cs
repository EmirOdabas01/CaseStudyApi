using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudyApi.BusinessLogic.ViewModels.ProductImageFile
{
    public class UploadProductImageFilesVM
    {
        public string Id { get; set; }
        public IFormFileCollection? Files { get; set; }
    }
}
