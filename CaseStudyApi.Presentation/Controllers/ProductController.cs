using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CaseStudyApi.BusinessLogic.ViewModels;
using CaseStudyApi.BusinessLogic.ViewModels.Product;
using CaseStudyApi.BusinessLogic.Interfaces.Product;
using System.Net;
using CaseStudyApi.BusinessLogic.ViewModels.ProductImageFile;
using CaseStudyApi.Presentation.Interfaces;
using CaseStudyApi.BusinessLogic.Interfaces.ProductImageFile;
namespace CaseStudyApi.Presentation.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IFileService _fileService;
        private readonly IProductImageFileService _productImageFileService;
        public ProductController(IProductService productService, 
            IFileService fileService, 
            IProductImageFileService productImageFileService)
        {
            _productService = productService;
            _fileService = fileService;
            _productImageFileService = productImageFileService;
        }

        [HttpPost]
        public async Task<IActionResult> Crate([FromBody] AddProductVM addProductVM)
        {
            var response = await _productService.AddProductAsync(addProductVM);
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var response = await _productService.GetProductByIdAsync(id);
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await _productService.GetAllProductsAsync();
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateProductVM updateProductVM)
        {
            var response = await _productService.UpdateProductAsync(updateProductVM);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var response = await _productService.RemoveProductAsync(id);
            return Ok(response);
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> Upload([FromRoute] int id)
        {
            var namesAndPaths = await _fileService.UploadAsync("resorce-images", Request.Form.Files, id);
            var response = await _productImageFileService.AddProductImageFilesAsync(namesAndPaths, id);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetImages([FromRoute] int id)
        {
            var productImages = await _productImageFileService.GetAllProductImageFilesAsync(id);
            return Ok(productImages);
        }

        [HttpDelete("{id}/{ImageId}")]
        public async Task<IActionResult> RemoveProductImages([FromRoute] RemoveProductImageFileVM removeProductImageFileVM)
        {
            await _productImageFileService.RemoveProductImageFilesAsync(removeProductImageFileVM);
            return Ok(); 
        }
    }
}

