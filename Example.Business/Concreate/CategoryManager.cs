#region

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Example.Business.Abstract;
using Example.Common.Attributes;
using Example.Common.Enums;
using Example.Common.Results;
using Example.Common.Results.Abstract;
using Example.Dal.Abstract.Repositories;
using Example.Entities.Dtos;
using Example.Entities.Entities;

#endregion

namespace Example.Business.Concreate
{
    [Log]
    public class CategoryManager : ICategoryManager
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryManager(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        [Auth("Category.List")]
        [Cache(Cache.AddOrGet, 5)]
        public async Task<IResult> GetCategories()
        {
            var categories = await _categoryRepository.GetList();
            var result = _mapper.Map<List<CategoryModel>>(categories);
            return new SuccessResult<List<CategoryModel>>(result);
        }

        [Cache(Cache.Remove, "ICategoryManager.Get")]
        public async Task<IResult> AddCategory(string name)
        {
            var category = new Category {Name = name};
            var result = await _categoryRepository.Add(category);
            var data = _mapper.Map<CategoryModel>(result);
            return new SuccessResult<CategoryModel>(data);
        }
    }
}