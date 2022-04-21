using Business.Abstract;
using Business.BusinessAspect.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Validation;
using Core.Aspects.Caching;
using Core.Aspects.Transaction;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        IProductDal _productDal;
        ICategoryService _categoryService;
     
        public ProductManager(IProductDal productDal, ICategoryService categoryService)
        {
            _productDal = productDal;
            _categoryService = categoryService;
         } 
     
        [SecuredOperation("product.add")]
        [ValidationAspect(typeof(ProductValidator),Priority =1)]
        [CacheRemoveAspect("IPorductService.Get")]
        public IResult Add(Product product)
        {
            // ValidationTool.Validate(new ProductValidator(), product);
            IResult result = BusinessRules.Run(CheckIfProductNameExists(product.ProductName), CheckIfProductCountCategoryCorrect(product.CategoryId),CheckIfCategoryLimitExceded());
            if (result!=null)
            {
                return result;
            }
            _productDal.Add(product);
                    return new SuccessResult(Messages.ProductAdded);
            } 
        [PerformanceAspect(5)]
        public IDataResult<List<Product>> GetAll()
        {
            if (DateTime.Now.Hour == 1)
            {

                return new ErrorDataResult<List<Product>>(Messages.MaintenanceTime);
            }
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(), Messages.ProductListed);
        }


        [CacheAspect(duration: 10)]
        public IDataResult<List<Product>> GetAllByCategoryId(int id)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.CategoryId == id));
        }
       
        public IDataResult<Product> GetAllById(int productId)
        {
            return new SuccessDataResult<Product>(_productDal.Get(p => p.ProductId == productId));
        }
       [PerformanceAspect(5)]
        public IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.UnitPrice >= min && p.UnitPrice <= max));
        }
        [CacheAspect(duration: 10)]
        public IDataResult<List<ProductDetailDTO>> GetProductDetail()
        {
            if (DateTime.Now.Hour== 20)
            {
                return new ErrorDataResult<List<ProductDetailDTO>>(Messages.MaintenanceTime);
            }
            return new SuccessDataResult<List<ProductDetailDTO>>(_productDal.GetProductDetails());
        }
        [SecuredOperation("product.update,admin")]
        [ValidationAspect(typeof(ProductValidator))]
        [CacheRemoveAspect("IPorductService.Get")]
        public IResult Update(Product product)
        {
            var result = _productDal.GetAll(p => p.CategoryId == product.CategoryId).Count;
            if (result >= 10)
            {
                return new ErrorResult(Messages.ProductCountCategoryError);
            }
            _productDal.Update(product);
            return new ErrorResult();
        }


      
        
        private IResult CheckIfProductCountCategoryCorrect(int categoryId)
        {
            var result = _productDal.GetAll(p => p.CategoryId == categoryId).Count;
            if (result >= 10)
            {
                return new ErrorResult(Messages.ProductCountCategoryError);
            }
            return new SuccessResult();

        }
        private IResult CheckIfProductNameExists(string productName)
        {
            var result = _productDal.GetAll(p => p.ProductName == productName).Any();
            if (result)
            {
                return new ErrorResult(Messages.ProductNameAlreadyExists);
            }
            return new SuccessResult();
        }
        private IResult CheckIfCategoryLimitExceded()
            {
            var result = _categoryService.GetAll();
            if (result.Data.Count>15)
            {
                return new ErrorResult(Messages.CategoryLimitExceded);
            }
            return new SuccessResult();
        }
        [TransactionScopeAspect]
        public IResult AddTranscationTest(Product product)
        {
            _productDal.Add(product);
            _productDal.Update(product);
            return new SuccessResult();
        }
    }
}
//if (product.UnitPrice<=0)
//{
//    return new ErrorResult(Messages.UnitPriceInvalid);
//}
//if (product.ProductName.Length<2)
//{
//    return new ErrorResult(Messages.ProductNameInvalid);
//}
//var context = new ValidationContext<Product>(product);
//ProductValidator productValidator = new ProductValidator();
//var result = productValidator.Validate(context);
//if (!result.IsValid)
//{
//    throw new ValidationException(result.Errors);
//}//spaghetti kod 

//_logger.Log();
//try
//{

//    _productDal.Add(product);

//    return new SuccessResult(Messages.ProductAdded);
//}
//catch (Exception exception)
//{
//    _logger.Log();
//}
//return new ErrorResult();
//if (product.CategoryId <= 10)
//{
//    _productDal.Add(product);

//    return new SuccessResult(Messages.ProductAdded);
//}
//else
//{
//    return new ErrorResult();
//}
