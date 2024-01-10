using BaseRepo.Interfaces;
using Fruit.Base.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fruit.Base.Interface
{
    public interface IComment : IRepository<Comment>
    {
        List<Comment> GetCommentListAllPaging(string search, int offset, int limit);
    }
}
