using Bussines.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebUI.ViewModels;

namespace WebUI.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductManager _productManager;
        public ProductController(IProductManager productManager)
        {
            _productManager = productManager;
        }

        public IActionResult Index()
        {
            return View();
        }
       

        public IActionResult Detail(int id)
        {
            try
            {

                var product = _productManager.GetProductById(id);
                var productCategory = product.Categories.Select(x => x.Id).ToList();
                var relateProducts = _productManager.RelatedProduct(productCategory, product.Id);
                ProductDetailVM productDetail = new()
                {
                    Product = product,
                    Products = relateProducts,
                };
                
                return View(productDetail);
            }
            catch (Exception)
            {
                        
                return NotFound();
            }
           
            
        }
    }
}
