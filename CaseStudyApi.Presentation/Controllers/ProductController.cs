using CaseStudyApi.BusinessLogic.Interfaces.Product;
using CaseStudyApi.BusinessLogic.Interfaces.ProductImageFile;
using CaseStudyApi.BusinessLogic.ViewModels;
using CaseStudyApi.BusinessLogic.ViewModels.Product;
using CaseStudyApi.BusinessLogic.ViewModels.ProductImageFile;
using CaseStudyApi.Presentation.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
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
        [Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> Create([FromBody] AddProductVM addProductVM)
        {
            var response = await _productService.AddProductAsync(addProductVM);
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
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
        [Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> Put([FromBody] UpdateProductVM updateProductVM)
        {
            var response = await _productService.UpdateProductAsync(updateProductVM);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var response = await _productService.RemoveProductAsync(id);
            return Ok(response);
        }

        [HttpPost("{id}")]
        [Authorize(AuthenticationSchemes = "Admin")]
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
        [Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> RemoveProductImages([FromRoute] RemoveProductImageFileVM removeProductImageFileVM)
        {
            await _productImageFileService.RemoveProductImageFilesAsync(removeProductImageFileVM);
            return Ok(); 
        }

        [HttpPut]
        [Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> UpdateShowcaseAndColor([FromBody] SetColorsAndShowCaseVM setColorsAndShowCaseVM)
        {
            await _productImageFileService.SetColorsAndShowCase(setColorsAndShowCaseVM);
            return Ok();
        }
    }
}

