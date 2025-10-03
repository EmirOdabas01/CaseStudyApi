using CaseStudyApi.BusinessLogic.Dtos.ProductImageFile;
using CaseStudyApi.BusinessLogic.Interfaces;
using CaseStudyApi.BusinessLogic.Interfaces.ProductImageFile;
using CaseStudyApi.BusinessLogic.Operations;
using CaseStudyApi.BusinessLogic.ViewModels.ProductImageFile;
using CaseStudyApi.DataAccess.Interfaces;
using CaseStudyApi.DataAccess.Repositories;
using CaseStudyApi.Domain.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Formats.Tar;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace CaseStudyApi.BusinessLogic.Services
{
    public class ProductImageFileService : IProductImageFileService
    {
        private readonly IProductImageFileReadRepository _productImageFileReadRepository;
        private readonly IProductImageFileWriteRepository _productImageFileWriteRepository;
        private readonly IProductReadRepository _productReadRepository;
        private readonly IProductWriteRepository _productWriteRepository;
        readonly IConfiguration configuration;
        public ProductImageFileService(IProductImageFileWriteRepository productImageFileWriteRepository
            , IProductImageFileReadRepository productImageFileReadRepository
            ,IProductReadRepository productReadRepository,
            IConfiguration configuration,
            IProductWriteRepository productWriteRepository)
        {
            _productImageFileWriteRepository = productImageFileWriteRepository;
            _productImageFileReadRepository = productImageFileReadRepository;
            _productReadRepository = productReadRepository;
            this.configuration = configuration;
            _productWriteRepository = productWriteRepository;
        }

        public async Task<int> AddProductImageFilesAsync(List<(string fileName, string path)> files,int id)
        {
            List<ProductImageFile> productImageFiles = new();
            productImageFiles.AddRange(files.Select(f => new ProductImageFile
            {
                Name = f.fileName,
                Path = f.path,
                ProductId = id
            }));

            await _productImageFileWriteRepository.AddRangeAsync(productImageFiles);
            return await _productImageFileWriteRepository.SaveAsync();
        }

        public async Task<List<ProductImageFileReadDto>?> GetAllProductImageFilesAsync(int id)
        {
            var product = await _productReadRepository.Table.Include(p => p.Images).FirstOrDefaultAsync(p => p.Id == id);
            return product?.Images.Select(p => new ProductImageFileReadDto
            {
                Id = p.Id,
                Name = p.Name,
                Path = $"{configuration["BaseStorageUrl"]}/{p.Path}"
            }).ToList();
        }

       
        public async Task RemoveProductImageFilesAsync(RemoveProductImageFileVM removeProductImageFileVM)
        {
            var product = await _productReadRepository.Table.Include(p => p.Images).FirstOrDefaultAsync(p => p.Id == removeProductImageFileVM.Id);
            var productImageFile = product?.Images.FirstOrDefault(p => p.Id == removeProductImageFileVM.ImageId);

            if (productImageFile != null)
                product?.Images.Remove(productImageFile);

            await _productWriteRepository.SaveAsync();
        }
    }
}
