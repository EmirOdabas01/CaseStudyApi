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
        Task<List<ProductImageFileReadDto>> GetAllProductImageFilesAsync();
        Task RemoveProductImageFilesAsync(List<int> ids);
        Task UpdateProductImageFilesAsync(List<UpdateProductImageFilesVM> updateProductImageFilesVM);
        Task AddProductImageFilesAsync(List<AddProductImageFilesVM> addProductImageFilesVM);
        Task<ProductImageFileReadDto?> GetProductImageFileByIdAsync(int id);
    }
}
