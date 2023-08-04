using Core.DataAccess.Mongo;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.MongoDb
{
    public class BlogDal : MongoRepositoryBase<Blog>, IBlogDal
    {
       
    }
}
