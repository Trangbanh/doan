using BaseRepo.Repositories;
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
	public class IplDetailedInvoice : Repository<DetailedInvoice>, IDetailedInvoice
	{
		public IConfiguration _configuration { get; set; }
		internal string _cnnString = string.Empty;
		public fruitkhaContext _dbContext;
		public IplDetailedInvoice(fruitkhaContext dbContext, IConfiguration configuration) : base(dbContext)
		{
			_dbContext = dbContext;
			_configuration = configuration;
			_cnnString = _configuration.GetConnectionString("DefaultConnection");
		}
	}
}
