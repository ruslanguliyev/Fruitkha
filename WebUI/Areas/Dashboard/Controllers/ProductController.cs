using Bussines.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;
using WebUI.Areas.Dashboard.ViewModel;

namespace WebUI.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    public class ProductController : Controller
    {
        private readonly IProductManager _productManager;
        private readonly ICategoryManager _categoryManager;
        private readonly IProductCategoryManager _productCategoryManager;

        public ProductController(IProductManager productManager, ICategoryManager categoryManager, IProductCategoryManager productCategoryManager)
        {
            _productManager = productManager;
            _categoryManager = categoryManager;
            _productCategoryManager = productCategoryManager;
        }

        public IActionResult Index()
        {
            var products = _productManager.GetAll();
            return View(products);
        }

        [HttpGet]

        public IActionResult Create()
        {
            ViewData["Categories"] = _categoryManager.GetAll();
            return View();
        }
        [HttpPost]

        public async Task<IActionResult> Create(Product product, IFormFile NewPhotoUrl, IFormFile NewCoverPhoto, List<int> Category)
        {
            string myPhoto = Guid.NewGuid().ToString() + Path.GetExtension(NewPhotoUrl.FileName);
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/image", myPhoto);
            using (var stream = new FileStream(path, FileMode.Create))
            {
                NewPhotoUrl.CopyTo(stream);
            }

            string myCoverPhoto = Guid.NewGuid().ToString() + Path.GetExtension(NewCoverPhoto.FileName);
            string pathCoverPhoto = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/image", myCoverPhoto);
            using (var stream = new FileStream(pathCoverPhoto, FileMode.Create))
            {
                NewCoverPhoto.CopyTo(stream);
            }


            product.CoverPhoto = "/image/" + myCoverPhoto;
            product.PhotoUrl = "/image/" + myPhoto;
            product.SeoUrl = "test";

            var pro = _productManager.Add(product);


            for (int i = 0; i < Category.Count; i++)
            {
                ProductCategory productCategory = new()
                {
                    CategoryId = Category[i],
                    ProductId = pro.Id
                };

                _productCategoryManager.AddProductCategory(productCategory);
            }

            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Delete(int Id)
        {
            var product = _productManager.GetById(Id);
            if (product == null)
                return NotFound();
            return View(product);
        }

        [HttpPost]
        public IActionResult Delete(Product product)
        {
            try
            {
                _productManager.Delete(product.Id);
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return NotFound();

            }
        }
        //[HttpPost]
        //public IActionResult Restore(int Id)
        //{
        //    Product product = _productManager.GetById(Id);
        //    return View(product);   
        //}

        [HttpGet]
        public IActionResult Update(int Id)
        {
            var product = _productManager.GetById(Id);
            var categories = _categoryManager.GetAll();
            var productCategories = _productCategoryManager.GetProductCategoriesById(Id);
            List<int> productCategoryId = new();
            foreach (var pc in productCategories)
            {
                productCategoryId.Add(pc.CategoryId);
            }
            ProductVM vm = new()
            {
                Product = product,
                Categories = categories,
                ProductCategories = productCategoryId,
            };


            return View(vm);

        }

        [HttpPost]
        public IActionResult Update(Product product,List<int> CategoriesId,IFormFile NewPhotoUrl, IFormFile NewCoverPhoto)
        {
            try
            {
                if (NewPhotoUrl != null)
                {
                    string myPhoto = Guid.NewGuid().ToString() + Path.GetExtension(NewPhotoUrl.FileName);
                    string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/image", myPhoto);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        NewPhotoUrl.CopyTo(stream);
                    }
                    product.PhotoUrl = "/image/" + myPhoto;
                }
                if (NewCoverPhoto != null)
                {
                    string myPhoto = Guid.NewGuid().ToString() + Path.GetExtension(NewCoverPhoto.FileName);
                    string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/image", myPhoto);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        NewCoverPhoto.CopyTo(stream);
                    }
                    product.CoverPhoto = "/image/" + myPhoto;
                }
                _productCategoryManager.RemoveProductCategories(product.Id);
                for (int i = 0; i < CategoriesId.Count; i++)
                {
                    ProductCategory productCategory = new()
                    {
                        CategoryId = CategoriesId[i],
                        ProductId = product.Id,
                    };
                    _productCategoryManager.AddProductCategory(productCategory);
                }
                product.SeoUrl = "test";
                _productManager.Update(product);
                return RedirectToAction("Index");
            }
            catch (Exception)
            {

                return View();
            }
        }
    }
}
