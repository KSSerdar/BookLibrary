using Core.Entities;
using DAL.Context;
using DAL.Repository;
using Microsoft.EntityFrameworkCore;
using Services.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Service
{
    public class AuthorService : GenericRepository<Author>, IAuthorService
    {
        private readonly APIContext _context;
        public AuthorService(APIContext commerceContext) : base(commerceContext)
        {
            _context = commerceContext;
        }


        public async Task<string> GetAuthorNameByID(Guid id)
        {
            var author=await _context.Authors.FirstOrDefaultAsync(x=>x.ID==id);
            return author.Name;
        }
    }
}
