using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussines.Abstract
{
    public interface IProductManager
    {
        Product Add(Product product);
        void Update(Product product);
        void Delete(int productId);
        List<Product> GetHomeProducts();
        List<Product> RelatedProduct(List<int> categoryId, int productId);
        List<Product> GetAll();
        Product Get(int Id);
        Product GetById(int Id);
        ProductDetailDTO GetProductById(int Id);
        List<Product> GetProductsByCategory(int categoryId);
        List<Product> GetSliderProducts();
        List<Product> GetShopProduct(int? categoryId, decimal? minPrice, decimal? maxPrice);
        void Restore(int productId);
    }
}
