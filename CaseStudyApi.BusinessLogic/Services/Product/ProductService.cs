using CaseStudyApi.BusinessLogic.Dtos.Product;
using CaseStudyApi.BusinessLogic.Interfaces.Product;
using CaseStudyApi.BusinessLogic.ViewModels.Product;
using CaseStudyApi.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudyApi.BusinessLogic.Services.Product
{
    public class ProductService : IProductService
    {
        private readonly IProductReadRepository _productReadRepository;
        private readonly IProductWriteRepository _productWriteRepository;

        public ProductService(IProductReadRepository productReadRepository,
            IProductWriteRepository productWriteRepository)
        {
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
        }


        public async Task<int> AddProductAsync(AddProductVM addProductVM)
        {
            await _productWriteRepository.AddAsync(new Domain.Entities.Product
            {
                Name = addProductVM.Name,
                PopularityScore = addProductVM.PopularityScore,
                Stock = addProductVM.Stock,
                Weight = addProductVM.Weight,

            });

            return await _productWriteRepository.SaveAsync();
        }
        public async Task<List<ProductReadDto>> GetAllProductsAsync()
        {
            var products =  _productReadRepository.GetAll(false).Select(p => new ProductReadDto
            {
              Name = p.Name,
              Stock = p.Stock,
              PopularityScore = p.PopularityScore,
              Weight = p.Weight,
            }).ToListAsync();

            return await products;
        }

        public async Task<ProductReadDto?> GetProductByIdAsync(int id)
        {
            var product = await _productReadRepository.GetByIdAsync(id, false);
            return new()
            {
                Name = product.Name,
                Stock = product.Stock,
                PopularityScore = product.PopularityScore,
                Weight = product.PopularityScore
            };
        }

        public async Task<int> RemoveProductAsync(int id)
        {
            await _productWriteRepository.RemoveAsync(id);
            return await _productWriteRepository.SaveAsync();
        }

        public async Task<int> UpdateProductAsync(UpdateProductVM updateProductVM)
        {
            var product = await _productReadRepository.GetByIdAsync(updateProductVM.Id);
            product.Stock = updateProductVM.Stock;
            product.Weight = updateProductVM.Weight;
            product.Name = updateProductVM.Name;
            product.PopularityScore = updateProductVM.PopularityScore;

            return await _productWriteRepository.SaveAsync();
        }
    }
}
