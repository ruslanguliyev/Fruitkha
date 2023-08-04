using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public  interface IProductDal : IEntityRepository<Product>
    {
        List<Product> GetByCategory(int categoryId);
        List<Product> GetAllHomeProduct();
        List<Product> RelatedProduct(List<int> categoryId, int productId);
        Product AddProduct(Product product);
        List<Product> GetFilterShopProduct(int? categoryId, decimal? minPrice, decimal? maxPrice);
        ProductDetailDTO GetProductById(int productId);
    }
}
