using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
   public  interface IProductService
    {
        IDataResult<List<Product>> GetAll();
       
        IDataResult<List<Product>> GetAllByCategoryId(int id);
        IDataResult<List<Product>>GetByUnitPrice(decimal min, decimal max);
        IDataResult<List<ProductDetailDTO>> GetProductDetail();
        IDataResult<Product> GetAllById(int productId);
        //IDataResult<Product> GetByCategoryId(int categoryId);



        IResult Add(Product product);
        IResult Update(Product product);
        IResult AddTranscationTest(Product product);
    }
}
