using BaseRepo.Interfaces;
using Fruit.Base.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fruit.Base.Interface
{
    public interface IProduct : IRepository<Product>
    {
        List<Product> GetProductListAllPaging(string search, int offset, int limit, int typeId = 0);
    }
}
