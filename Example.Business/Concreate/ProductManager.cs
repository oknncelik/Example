#region

using System.Collections.Generic;
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

namespace Example.Business.Concreate;

public class ProductManager : IProductManager
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public ProductManager(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    [Auth("Product.List")]
    [Cache(Cache.AddOrGet, 1)]
    public async Task<IResult> GetProducts()
    {
        var result = await _productRepository.GetList();
        var data = _mapper.Map<List<ProductModel>>(result);
        return new SuccessResult<List<ProductModel>>(data);
    }

    [Auth("Product.List")]
    [Cache(Cache.AddOrGet, 1)]
    public async Task<IResult> GetProductById(int id)
    {
        var result = await _productRepository.Get(x => x.Id == id);
        var data = _mapper.Map<ProductModel>(result);
        return new SuccessResult<ProductModel>(data);
    }

    [Auth("Product.Add")]
    [Cache(Cache.Remove, "IProductManager.Get")]
    public async Task<IResult> AddProduct(ProductModel product)
    {
        var entity = _mapper.Map<Product>(product);
        var result = await _productRepository.Add(entity);
        var data = _mapper.Map<ProductModel>(result);
        return new SuccessResult<ProductModel>(data);
    }

    [Auth("Product.Update")]
    [Cache(Cache.Remove, "IProductManager.Get")]
    public async Task<IResult> UpdateProduct(ProductModel product)
    {
        var entity = _mapper.Map<Product>(product);
        var result = await _productRepository.Update(entity);
        var data = _mapper.Map<ProductModel>(result);
        return new SuccessResult<ProductModel>(data);
    }

    [Auth("Product.Delete")]
    [Cache(Cache.Remove, "IProductManager.Get")]
    public async Task<IResult> DeleteProduct(ProductModel product)
    {
        var entity = _mapper.Map<Product>(product);
        var result = await _productRepository.Delete(entity);
        return new SuccessResult<bool>(result);
    }

    [Auth("Product.List")]
    [Cache(Cache.AddOrGet, 1)]
    public async Task<IResult> GetProductByCategory(int categoryId)
    {
        var result = await _productRepository.GetList(x => x.CategoryId == categoryId);
        var data = _mapper.Map<List<ProductModel>>(result);
        return new SuccessResult<List<ProductModel>>(data);
    }
}