using Core.Entities;
using DAL.Context;
using DAL.Repository;
using Services.IService;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Service
{
    public class CommentService : GenericRepository<Comment>, ICommentService
    {
        private readonly APIContext _context;
        public CommentService(APIContext commerceContext) : base(commerceContext)
        {
            _context = commerceContext;
        }

        public async Task<Comment> AddCommentAsync(Comment comment)
        {
            _context.Replies.Add(comment);
            await _context.SaveChangesAsync();
            return comment;
        }

        public async Task<IEnumerable<Comment>> GetCommentsByBookAsync(Guid bookId)
        {
            var result =await _context.Replies.Where(c=>c.BookID==bookId).ToListAsync();
            return result;
        }
    }
}
