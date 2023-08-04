using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class CategoryDal : EfEntityRepositoryBase<Category, ShopDbContext>, ICategoryDal
    {
        public Category RepeatName(Category category)
        {
            using ShopDbContext _context = new();
            var repeatName = _context.Categories.FirstOrDefault(c => c.Name == category.Name);
            return repeatName;
        }
    }
}
