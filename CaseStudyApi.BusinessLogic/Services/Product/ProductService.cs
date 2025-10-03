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

        private async Task<decimal> GetGoldPrice()
        {
            var httpClient = new HttpClient();
            var goldService = new GoldPriceService(httpClient, "goldapi-1cbgh19mg9id3bo-io");

            var price = await goldService.GetGramPricesAsync();

            return price;
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
            decimal goldPrice = await GetGoldPrice();
            var products =  _productReadRepository.GetAll(false).Select(p => new ProductReadDto
            {
              Name = p.Name,
              Stock = p.Stock,
              PopularityScore = p.PopularityScore,
              Weight = p.Weight,
              Price = (decimal)(p.PopularityScore + 1) * (decimal)p.Weight * goldPrice,
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
                Weight = product.PopularityScore,
                Price = await GetGoldPrice() * (decimal)product.Weight * (decimal)(product.PopularityScore + 1)
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

        public async Task<int?> GetProductImageCount(int id)
        {
            Domain.Entities.Product? product = await _productReadRepository.Table.Include(p => p.Images).FirstOrDefaultAsync(p => p.Id == id);
            int? imageCount = product?.Images.Count;

            return imageCount;
        }
    }
}
