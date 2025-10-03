using CaseStudyApi.BusinessLogic.Dtos.Product;
using CaseStudyApi.BusinessLogic.Dtos.ProductImageFile;
using CaseStudyApi.BusinessLogic.ViewModels.Product;
using CaseStudyApi.BusinessLogic.ViewModels.ProductImageFile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudyApi.BusinessLogic.Interfaces.ProductImageFile
{
    public interface IProductImageFileService
    {
        Task<List<ProductImageFileReadDto>?> GetAllProductImageFilesAsync(int id);
        Task RemoveProductImageFilesAsync(RemoveProductImageFileVM removeProductImageFileVM);
        Task<int> AddProductImageFilesAsync(List<(string fileName, string path)> files, int id);
    }
}
