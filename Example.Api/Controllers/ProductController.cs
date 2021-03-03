#region

using System.Threading.Tasks;
using Example.Business.Abstract;
using Example.Entities.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace Example.Api.Controllers
{
    [Authorize]
    [Route("api/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductManager _productManager;

        public ProductController(IProductManager productManager)
        {
            _productManager = productManager;
        }

        /// <summary>
        /// List of product
        /// </summary>
        /// <returns></returns>
        [HttpGet("products")]
        public async Task<IActionResult> GetProducts()
        {
            return Ok(await _productManager.GetProducts());
        }

        /// <summary>
        ///  Add product
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPost("add")]
        public async Task<IActionResult> AddProduct(ProductModel product)
        {
            return Ok(await _productManager.AddProduct(product));
        }

        /// <summary>
        /// Update product
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPut("update")]
        public async Task<IActionResult> UpdateProduct(ProductModel product)
        {
            return Ok(await _productManager.UpdateProduct(product));
        }

        /// <summary>
        /// Delete product
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteProduct(ProductModel product)
        {
            return Ok(await _productManager.DeleteProduct(product));
        }

        /// <summary>
        /// List of product by category
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        [HttpGet("getProductsByCategory")]
        public async Task<IActionResult> GetProductsByCategory(int categoryId)
        {
            return Ok(await _productManager.GetProductByCategory(categoryId));
        }
        
        /// <summary>
        /// Test
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetTest")]
        public async Task<IActionResult> GetTest()
        {
            return Ok("Ok.");
        }        
        
    }
}