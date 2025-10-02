using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CaseStudyApi.BusinessLogic.ViewModels;
using CaseStudyApi.BusinessLogic.ViewModels.Product;
using CaseStudyApi.BusinessLogic.Interfaces.Product;
using System.Net;
namespace CaseStudyApi.Presentation.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
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
    }
}

