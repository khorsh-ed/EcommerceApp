using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using Core.Interfaces;

namespace API.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _repo;

        public ProductsController(IProductRepository repo)
        {
            _repo = repo;

        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> Getproducts()
        {
            var products = await _repo.GetProductsAsync();
            return Ok(products);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> Getproduct(int id)
        {
            return await _repo.GetProductbyIdAsync(id);
        }

        [HttpGet("brands")]
        public async Task<ActionResult<List<ProductBrand>>> GetproductsBrand()
        {
             return Ok(await _repo.GetProductsBrandAsync());
        }

         [HttpGet("types")]
        public async Task<ActionResult<List<ProductType>>> GetproductsTypes()
        {
              return Ok(await _repo.GetProductsBrandAsync());
        }


    }
}