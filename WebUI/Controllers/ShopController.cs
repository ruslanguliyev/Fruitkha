using Bussines.Abstract;
using Microsoft.AspNetCore.Mvc;
using WebUI.ViewModels;

namespace WebUI.Controllers
{
    public class ShopController : Controller
    {
        private readonly ICategoryManager _categoryManager;
        private readonly IProductManager _productManager;
        public ShopController(ICategoryManager categoryManager, IProductManager productManager)
        {
            _categoryManager = categoryManager;
            _productManager = productManager;
        }

        public IActionResult Index(int? categoryId,decimal? minPrice, decimal? maxPrice)
        {
            var products = _productManager.GetShopProduct(categoryId,minPrice,maxPrice);
            var categories = _categoryManager.GetAll();
            ShopVM shopVM = new()
            {
                Products = products,
                Categories = categories
            };
            return View(shopVM);
        }
    }
}
