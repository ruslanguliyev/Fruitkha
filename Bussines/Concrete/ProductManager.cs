using Bussines.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussines.Concrete
{
    public class ProductManager : IProductManager

    {
        private readonly IProductDal _productDal;

        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }

        public Product Add(Product product)
        {
            return _productDal.AddProduct(product);
        }

        public void Delete(int productId)
        {
            var product = _productDal.Get(x => x.Id == productId);
            product.IsDelete = true;
            _productDal.Update(product);
        }

        public Product Get(int Id)
        {
            return _productDal.Get(x => x.Id == Id);
        }

        public List<Product> GetAll()
        {
            return _productDal.GetAll();
        }

        public Product GetById(int Id)
        {
            return _productDal.Get(x => x.Id == Id);
        }

        public List<Product> GetHomeProducts()
        {
            return _productDal.GetAllHomeProduct();
        }

        public ProductDetailDTO GetProductById(int Id)
        {
            return _productDal.GetProductById(Id);
        }

        public List<Product> GetProductsByCategory(int categoryId)
        {
            return _productDal.GetByCategory(categoryId);
        }

        public List<Product> GetShopProduct(int? categoryId, decimal? minPrice, decimal? maxPrice)
        {
            return _productDal.GetFilterShopProduct(categoryId,minPrice,maxPrice);
        }

        public List<Product> GetSliderProducts()
        {
            return _productDal.GetAll(x => x.IsSlider == true && x.IsDelete == false);
        }

        public List<Product> RelatedProduct(List<int> categoryId, int productId)
        {
            return _productDal.RelatedProduct(categoryId, productId);   
        }

        public void Restore(int productId)
        {
            var product = _productDal.Get(x => x.Id == productId);
            product.IsDelete = false;
            _productDal.Update(product);
        }

        public void Update(Product product)
        {
            _productDal.Update(product);
        }
    }
}
