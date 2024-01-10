using BaseRepo.Interfaces;
using Fruit.Base.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fruit.Base.Interface
{
    public interface IUser:IRepository<User>
    {
        List<User> GetUserListAllPaging(string search, int offset, int limit);
    }
}
