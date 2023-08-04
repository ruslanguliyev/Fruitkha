using Bussines.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussines.Concrete
{
    public class CategoryManager : ICategoryManager
    {
        private readonly ICategoryDal _categoryDal;

        public CategoryManager(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }

        public void Add(Category category)
        {
           _categoryDal.Add(category);
        }

        public List<Category> GetAll()
        {
            return _categoryDal.GetAll();
        }

        public Category GetByCategoryId(int id)
        {
            return _categoryDal.Get(x=>x.Id == id);
        }

        public void Remove(Category category)
        {
            _categoryDal.Delete(category);
        }

        public Category RepeatName(Category category)
        {
           return _categoryDal.RepeatName(category);
        }

        public void Update(Category category)
        {
            _categoryDal.Update(category);
        }
        
    }
}
