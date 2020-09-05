using API.Dtos;
using AutoMapper;
using Core.Entities;
using Microsoft.Extensions.Configuration;

namespace API.Helper
{
    public class ProductUrlResolver : IValueResolver<Product, ProductToReturnDto, string>
    {

        public readonly IConfiguration _Config ;
        public ProductUrlResolver(IConfiguration config)
        {
            _Config = config;
        }


      

        public string Resolve(Product source, ProductToReturnDto destination, string destMember, ResolutionContext context)
        {
            if(!string.IsNullOrEmpty(source.PictureUrl))
            {
                return _Config["ApiUrl"] + source.PictureUrl ;
            }
            return null ;
        }
    }
}