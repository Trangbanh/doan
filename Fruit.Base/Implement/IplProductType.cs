using BaseRepo.Repositories;
using Dapper;
using Fruit.Base.Interface;
using Fruit.Base.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fruit.Base.Implement
{
    public class IplProductType:Repository<ProductType>, IProductType
    {
        public IConfiguration _configuration { get; set; }
        internal string _cnnString = string.Empty;
        public fruitkhaContext _dbContext;
        public IplProductType(fruitkhaContext dbContext, IConfiguration configuration) : base(dbContext)
        {
            _dbContext = dbContext;
            _configuration = configuration;
            _cnnString = _configuration.GetConnectionString("DefaultConnection");
        }

        public List<ProductType> GetProductTypeListAll(string search, int offset, int limit)
        {
            var list = new List<ProductType>();
            var unitOfWork = new UnitOfWorkFactory(_cnnString);
            try
            {
                using (var u = unitOfWork.Create(false))
                {
                    var p = new DynamicParameters();
                    p.Add("@search", search);
                    p.Add("@offset", offset);
                    p.Add("@limit", limit);
                    list = u.GetIEnumerable<ProductType>("Sp_GetProductType", p).ToList();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return list;
        }
    }
}
