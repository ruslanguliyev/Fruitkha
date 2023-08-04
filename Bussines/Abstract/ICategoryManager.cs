﻿using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussines.Abstract
{
    public interface ICategoryManager
    {
        void Add(Category category);
        void Remove(Category category);
        void Update(Category category);
        Category RepeatName(Category category);
        List<Category> GetAll();
        Category GetByCategoryId(int id);

    }
}
