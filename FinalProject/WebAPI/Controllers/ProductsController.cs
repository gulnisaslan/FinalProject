using Business.Abstract;
using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        //loosely coupled
        //naming Convetion
        //Ioc Container -->inversion of control
       
        IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            //Thread.Sleep(1000);
         //   dependecy chain


            var result =  _productService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        [HttpPost("add") ]
        public IActionResult Add(Product product)

        {
            var result = _productService.Add(product);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);

        }
        [HttpPost("getbyId")]
        public IActionResult GetById(int id )
        {
            var result = _productService.GetAllById(id);
            if (result.Success)
            {
                return Ok(result.Success);
            }
            return BadRequest(result);

        }
        [HttpPost("getbyCategoryId")]
        public IActionResult GetByCategory(int categoryId)
        {
            var result = _productService.GetAllById(categoryId);
            if (result.Success)
            {
                return Ok(result.Success);
            }
            return BadRequest(result);

        }
    }
}
