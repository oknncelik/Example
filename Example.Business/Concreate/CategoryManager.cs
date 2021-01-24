using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Example.Business.Abstract;
using Example.Common.Attributes;
using Example.Common.Enums;
using Example.Common.Results;
using Example.Common.Results.Abstract;
using Example.Dal.Abstract.Repositories;
using Example.Entities.Dtos;
using Example.Entities.Entities;

namespace Example.Business.Concreate
{
    public class CategoryManager : ICategoryManager
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryManager(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [Auth("Category.List")]
        [Cache(Cache.AddOrGet, 5)]
        public async Task<IResult> GetCategories()
        {
            var categories = await _categoryRepository.GetList();
            var result = categories.Select(x => new CategoryModel
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();
            return new SuccessResult<List<CategoryModel>>(result);
        }

        [Cache(Cache.Remove, "ICategoryManager.Get")]
        public async Task<IResult> AddCategory(string name)
        {
            var result = await _categoryRepository.Add(new Category {Name = name});
            return new SuccessResult<CategoryModel>(new CategoryModel
            {
                Id = result.Id,
                Name = result.Name
            });
        }
    }
}