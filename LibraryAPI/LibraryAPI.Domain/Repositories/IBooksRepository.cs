using LibraryAPI.Domain.Entities;
using LibraryAPI.Domain.Services.Page;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryAPI.Domain.Repositories
{
    public interface IBooksRepository
    {
        IQueryable<Book> GetAll(LibraryQuery query);
    }
}
