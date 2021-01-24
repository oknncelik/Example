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
    public class ProductManager  : IProductManager
    {
        private readonly IProductRepository _productRepository;

        public ProductManager(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [Cache(Cache.AddOrGet, 1)]
        public async Task<IResult> GetProducts()
        {
            var result = await _productRepository.GetList();
            return new SuccessResult<List<ProductModel>>(result.Select(x=> new ProductModel
            {
               Id = x.Id,
               Name = x.Name,
               CategoryId = x.CategoryId,
               Price = x.Price
            }).ToList());
        }

        [Cache(Cache.AddOrGet, 1)]
        public async Task<IResult> GetProductById(int id)
        {
            var result = await _productRepository.Get(x=> x.Id == id);
            return new SuccessResult<ProductModel>(new ProductModel
            {
                Id = result.Id,
                Name = result.Name,
                CategoryId = result.CategoryId,
                Price = result.Price
            });
        }
        
        [Cache(Cache.Remove, "IProductManager.Get")]
        public async Task<IResult> AddProduct(ProductModel product)
        {
            var result = await _productRepository.Add(new Product
            {
               Name = product.Name,
               CategoryId = product.CategoryId,
               Price = product.Price
            });
            return new SuccessResult<ProductModel>(new ProductModel
            {
                Id = result.Id,
                Name = result.Name,
                CategoryId = result.CategoryId,
                Price = result.Price
            });
        }

        [Cache(Cache.Remove, "IProductManager.Get")]
        public async Task<IResult> UpdateProduct(ProductModel product)
        {
            var result = await _productRepository.Update(new Product
            {
                Id = product.Id,
                Name = product.Name,
                CategoryId = product.CategoryId,
                Price = product.Price
            });
            return new SuccessResult<ProductModel>(new ProductModel
            {
                Id = result.Id,
                Name = result.Name,
                CategoryId = result.CategoryId,
                Price = result.Price
            });
        }

        [Cache(Cache.Remove, "IProductManager.Get")]
        public async Task<IResult> DeleteProduct(ProductModel product)
        {
            var result = await _productRepository.Delete(new Product
            {
                Id = product.Id,
                Name = product.Name,
                CategoryId = product.CategoryId,
                Price = product.Price
            });
            return new SuccessResult<bool>(result);
        }

        [Cache(Cache.AddOrGet, 1)]
        public async Task<IResult> GetProductByCategory(int categoryId)
        {
            var result = await _productRepository.GetList(x=> x.CategoryId == categoryId);
            return new SuccessResult<List<ProductModel>>(result.Select(x=> new ProductModel
            {
                Id = x.Id,
                Name = x.Name,
                CategoryId = x.CategoryId,
                Price = x.Price
            }).ToList());
        }
    }
}