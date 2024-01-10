using Fruit.Base.Implement;
using Fruit.Base.Interface;
using Fruit.Base.Models;
using Microsoft.Extensions.Configuration;

namespace FruitKha.Base
{
	public class IplBase : IBase
    {
        #region contructor
        private fruitkhaContext _dbContext;
        public IConfiguration _configuration { get; }

        public IplBase(fruitkhaContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }
        #endregion
        #region repository   
        private IAdmin _admins;
        public IAdmin admins
        {
            get
            {
                return _admins ?? (_admins = new IplAdmin(_dbContext, _configuration));
            }
        }
        private IUser _user;
        public IUser users
        {
            get
            {
                return _user ?? (_user = new IplUser(_dbContext, _configuration));
            }
        }
        private IProductType _productType;
        public IProductType ProductType
        {
            get
            {
                return _productType ?? (_productType = new IplProductType(_dbContext, _configuration));
            }
        }
        private IProduct _products;
        public IProduct products
        {
            get
            {
                return _products ?? (_products = new IplProduct(_dbContext, _configuration));
            }
        }
        private IBlog _blogs;
        public IBlog blogs
        {
            get
            {
                return _blogs ?? (_blogs = new IplBlog(_dbContext, _configuration));
            }
        }
        private IBill _bills;
        public IBill bills
        {
            get
            {
                return _bills ?? (_bills = new IplBill(_dbContext, _configuration));
            }
        }
        private IComment _comment;
        public IComment comments
        {
            get
            {
                return _comment ?? (_comment = new IplComment(_dbContext, _configuration));
            }
        }
        private IDetailedInvoice _detailedInvoice;
		public IDetailedInvoice detailedInvoice
        {
			get
			{
				return _detailedInvoice ?? (_detailedInvoice = new IplDetailedInvoice(_dbContext, _configuration));
			}
		}
        private IContacts _contacts;
        public IContacts contacts
        {
            get
            {
                return _contacts ?? (_contacts = new IplContacts(_dbContext, _configuration));
            }
        }

        public void Commit()
        {
            _dbContext.SaveChanges();
        }
        #endregion
    }
}
