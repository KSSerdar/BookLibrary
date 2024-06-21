using Core.Entities;
using DAL.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.IService
{
    public interface IAuthorService:IRepository<Author>
    {
        Task<string> GetAuthorNameByID(Guid id);
    }
}
