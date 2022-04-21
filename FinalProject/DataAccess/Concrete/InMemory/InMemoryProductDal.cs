using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.InMemory
{
    public class EfProductDall : IProductDal
    {
        List<Product> _products;
        public EfProductDall()
        {
            _products = new List<Product>
            {
                new Product
                {
                    ProductId=1,
                    CategoryId=1,
                    ProductName="Bardak",
                    UnitPrice = 15,
                    UnitsInStock=15
                },
                 new Product
                {
                    ProductId=2,
                    CategoryId=1,
                    ProductName="Kamera",
                    UnitPrice = 500,
                    UnitsInStock=7
                } ,

                new Product
                {
                    ProductId=3,
                    CategoryId=2,
                    ProductName="Klavye",
                    UnitPrice = 1500,
                    UnitsInStock=6
                }
                ,new Product
                {
                    ProductId=4,
                    CategoryId=2,
                    ProductName="Mause",
                    UnitPrice = 50,
                    UnitsInStock=4
                }
                ,new Product
                {
                    ProductId=5,
                    CategoryId=2,
                    ProductName="telefon",
                    UnitPrice = 60,
                    UnitsInStock=3
                }
            };
        }
        //linq=language integrated query
        //lambda
        public void Add(Product product)
        {
        
            _products.Add(product);
        }

        public void Delete(Product product)
        {
           //.SingleOrDefault= methodur
           Product productToDelete = _products.SingleOrDefault(p => p.ProductId == product.ProductId);
            _products.Remove(product);
        }

        public Product Get(Expression<Func<Product, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetAll()
        {
            return _products;
        }

        public List<Product> GetAll(Expression<Func<Product, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetAllByCategory(int categoryId)
        {
           return  _products.Where(p => p.CategoryId == categoryId).ToList();
        }

        public List<ProductDetailDTO> GetProductDetails()
        {
            throw new NotImplementedException();
        }

        public void Update(Product product)
        {
            //Gönderdiği urun ıd'sine sahip olan listedeki Urunu bul

            Product productToUpdate = _products.SingleOrDefault(p => p.ProductId == product.ProductId);
            productToUpdate.ProductName = product.ProductName;
            productToUpdate.CategoryId = product.CategoryId;
            productToUpdate.UnitPrice = product.UnitPrice;
            productToUpdate.UnitsInStock = product.UnitsInStock;



        }
    }
}
//Product productToDelete = new Product(); Hatalı kod
//Product productToDelete = null;

//foreach (var p in _products)
//{
//    if (product.ProductId == p.ProductId)
//    {
//        productToDelete = p;
//    }

//} not: bunları kullanmayan gerek yok
