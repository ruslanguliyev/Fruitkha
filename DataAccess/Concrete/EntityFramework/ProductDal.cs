using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class ProductDal : EfEntityRepositoryBase<Product, ShopDbContext>, IProductDal
    {
        public Product AddProduct(Product product)
        {
            using ShopDbContext _context = new(); // garbage
            _context.Products.Add(product);
            _context.SaveChanges();
            return product;
        }

        public List<Product> GetAllHomeProduct()
        {
            using ShopDbContext _context = new();
            return _context.Products.Where(x => x.IsDelete == false).Take(3).ToList();
        }

        public List<Product> GetByCategory(int categoryId)
        {
            using ShopDbContext _context = new();
            var products = _context.ProductCategories.Where(c => c.CategoryId == categoryId).ToList();
            List<Product> product = new();

            foreach (var pc in products)
            {
                var findedProduct = _context.Products.FirstOrDefault(x => x.Id == pc.ProductId);
                product.Add(findedProduct);
            }
            return product;

        }

        public List<Product> GetFilterShopProduct(int? categoryId, decimal? minPrice, decimal? maxPrice)
        {
            using var _context = new ShopDbContext();
            List<Product> products = new();
            foreach (var item in _context.Products.ToList())
            {
                products.Add(item);
            }
            var findMaxPrice = products.OrderByDescending(x => x.Price).FirstOrDefault().Price;
            if (minPrice == null)
               minPrice = 0; 
            if (maxPrice == null)
                maxPrice = findMaxPrice;   

            if (categoryId != null)
            {
                var productCategory = _context.ProductCategories.Where(x => x.CategoryId == categoryId).ToList();
                List<Product> result = new();
                for (int i = 0; i < productCategory.Count; i++)
                {
                    var product = _context.Products.FirstOrDefault(x => x.Id == productCategory[i].ProductId && x.Price >= minPrice && x.Price <= maxPrice);
                    if(product != null)
                        result.Add(product);
                }
                return result;
            }


            return _context.Products.ToList();
        }

        public ProductDetailDTO GetProductById(int productId)
        {
            using ShopDbContext _context = new();
            var productCategory = _context.ProductCategories.Include(x=>x.Category).Where(p => p.ProductId == productId).ToList();
            var product = _context.Products.FirstOrDefault(x => x.Id == productId);
            List<Category> categoryList = new();

            foreach (var item in productCategory)
            {
                categoryList.Add(item.Category);
            }

            ProductDetailDTO result = new()
            {
                Id = product.Id,
                Name = product.Name,
                CoverPhoto = product.CoverPhoto,
                Description = product.Description,
                PhotoUrl = product.PhotoUrl,
                Price = product.Price,
                Quantity = product.Quantity,
                Categories = categoryList
            };

            return result;

        }

        public List<Product> RelatedProduct(List<int> categoryId, int productId)
            {
            using var _context = new ShopDbContext();
            var productCategory = _context.ProductCategories.Where(p => p.CategoryId == categoryId[0]).Include(x => x.Product);
            List<Product> products = new();
            for (int i = 0; i < productCategory.ToList().Count; i++)
            {
                products.Add(productCategory.Skip(i).FirstOrDefault().Product);
            }
            return products.Where(x => x.Id != productId).Take(3).ToList();
        }
    }
}
