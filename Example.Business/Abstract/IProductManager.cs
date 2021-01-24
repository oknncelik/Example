﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Example.Common.Results.Abstract;
using Example.Entities.Dtos;
using Example.Entities.Entities;

namespace Example.Business.Abstract
{
    public interface IProductManager
    {
        Task<IResult> GetProducts();
        Task<IResult> GetProductById(int id);
        Task<IResult> AddProduct(ProductModel product);
        Task<IResult> UpdateProduct(ProductModel product);
        Task<IResult> DeleteProduct(ProductModel product);
        Task<IResult> GetProductByCategory(int categoryId);
    }
}