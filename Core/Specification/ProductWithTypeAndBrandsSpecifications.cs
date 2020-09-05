using Core.Entities;

namespace Core.Specification
{
    public class ProductWithTypeAndBrandsSpecifications : BaseSpecification<Product>
    {
        public ProductWithTypeAndBrandsSpecifications()
        {
            AddInclude (x=>x.ProductType);
            AddInclude (x=>x.ProductBrand);

        }

        public ProductWithTypeAndBrandsSpecifications(int id):base(x=>x.Id == id)
        {
            AddInclude (x=>x.ProductType);
            AddInclude (x=>x.ProductBrand);

        }
    }
}