using Core.Data;
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
    public class BookService : GenericRepository<Book>, IBookService
    {
      private readonly APIContext _context;
        public BookService(APIContext context):base(context) 
        {
            _context = context;
        }

        public async Task AddNewBook(NewBook book)
        {
            var newBook = new Book()
            {
                Name=book.Name,
                PublishDate=book.PublishDate,
                PictureURL=book.PictureURL,
                Description=book.Description,
                AuthorID=book.AuthorsID
            };
            await _context.Books.AddAsync(newBook);
            await _context.SaveChangesAsync();
           
        }

        public async Task<Book> GetBookByID(Guid id)
        {
            var result=await _context.Books.Include(c=>c.Replies).Include(c=>c.Author).FirstOrDefaultAsync(c=>c.ID==id);
            return result;
        }



        public async Task UpdateNewBook(NewBook book)
        {
            var bookdb = await _context.Books.FirstOrDefaultAsync(c => c.ID != book.ID);
            if (bookdb!=null)
            {
                bookdb.Name = book.Name;
                bookdb.PublishDate = book.PublishDate;
                bookdb.Description = book.Description;
                bookdb.AuthorID = book.AuthorsID;
                await _context.SaveChangesAsync();
            }
          
        }
    }
}
