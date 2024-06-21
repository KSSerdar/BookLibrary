using Core.Data;
using Core.Entities;
using DAL.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.IService
{
    public interface IBookService:IRepository<Book>
    {
       Task AddNewBook(NewBook book);
       Task UpdateNewBook(NewBook book);
        Task<Book> GetBookByID(Guid id);
    }
}
