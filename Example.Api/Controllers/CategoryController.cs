#region

using System.Threading.Tasks;
using Example.Business.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace Example.Api.Controllers
{
    [Authorize]
    [Route("api/category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryManager _categoryManager;

        public CategoryController(ICategoryManager categoryManager)
        {
            _categoryManager = categoryManager;
        }

        /// <summary>
        ///     List of Category
        /// </summary>
        /// <returns></returns>
        [HttpGet("categories")]
        public async Task<IActionResult> GetCategories()
        {
            return Ok(await _categoryManager.GetCategories());
        }

        /// <summary>
        ///     Add Category
        /// </summary>
        /// <param name="categoryName"></param>
        /// <returns></returns>
        [HttpPost("add")]
        public async Task<IActionResult> AddCategory(string categoryName)
        {
            return Ok(await _categoryManager.AddCategory(categoryName));
        }
    }
}