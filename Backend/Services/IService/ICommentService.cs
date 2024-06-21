using Core.Entities;
using DAL.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.IService
{
    public  interface ICommentService:IRepository<Comment>
    {
        Task<IEnumerable<Comment>> GetCommentsByBookAsync(Guid bookId);
        Task<Comment> AddCommentAsync(Comment comment);
    }
}
