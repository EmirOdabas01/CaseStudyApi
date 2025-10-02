using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CaseStudyApi.BusinessLogic.Dtos.Product;
using CaseStudyApi.BusinessLogic.ViewModels.Product;
using CaseStudyApi.Domain.Entities;
namespace CaseStudyApi.BusinessLogic.Interfaces.Product
{
    public interface IProductService
    {
        Task<List<ProductReadDto>> GetAllProductsAsync();
        Task<int> RemoveProductAsync(int id);
        Task<int> UpdateProductAsync(UpdateProductVM updateProductVM);
        Task<int> AddProductAsync(AddProductVM addProductVM);
        Task<ProductReadDto?> GetProductByIdAsync(int id);
    }
}
