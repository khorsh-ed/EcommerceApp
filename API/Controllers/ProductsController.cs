using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using Core.Interfaces;
using Core.Specification;
using AutoMapper;
using API.Dtos;
using Microsoft.AspNetCore.Http;
using API.Errors;
using Core.Specifications;
using API.Helpers;

namespace API.Controllers
{
    
    public class ProductsController : BaseApiController
    {
        private readonly IGenericRepository<Product> _productsRepo;
        private readonly IGenericRepository<ProductBrand> _productBrandRepo;
        public IGenericRepository<ProductType> _ProductTypeRepo ;

        private readonly IMapper _mapper;

        public ProductsController(IGenericRepository<Product> ProductsRepo ,
                                  IGenericRepository<ProductBrand> ProductBrandRepo,
                                  IGenericRepository<ProductType> ProductTypeRepo,
                                   IMapper mapper )
        {
            _productsRepo = ProductsRepo;
            _productBrandRepo = ProductBrandRepo;
            _ProductTypeRepo = ProductTypeRepo;
            _mapper = mapper ;
        }

          
        [HttpGet]
        public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts(
            [FromQuery]ProductSpecParams productParams)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(productParams);

            var countSpec = new ProductWithFiltersForCountSpecificication(productParams);

            var totalItems = await _productsRepo.CountAsync(countSpec);

            var products = await _productsRepo.ListAsync(spec);

            var data = _mapper
                .Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products);

            return Ok(new Pagination<ProductToReturnDto>(productParams.PageIndex, productParams.PageSize, totalItems, data));
        }


        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType( typeof(ApiResponse),StatusCodes.Status404NotFound) ]
        public async Task<ActionResult<Product>> Getproduct(int id)
        {
            var spec = new ProductWithTypeAndBrandsSpecifications(id);

            var product =  await _productsRepo.GetEntitywithspec(spec);

            if(product == null) return NotFound(new ApiResponse(404));

            return Ok(_mapper.Map<Product , ProductToReturnDto>(product) ) ;
        }

        [HttpGet("brands")]
        public async Task<ActionResult<List<ProductBrand>>> GetproductsBrand()
        {
             return Ok(await _productBrandRepo.ListAllAsync());
        }

         [HttpGet("types")]
        public async Task<ActionResult<List<ProductType>>> GetproductsTypes()
        {
              return Ok(await _ProductTypeRepo.ListAllAsync());
        }


    }
}