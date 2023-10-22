using AutoMapper;
using AutoMapper.QueryableExtensions;
using LibraryAPI.Application.Models.Books;
using LibraryAPI.Application.Services.Extensions;
using LibraryAPI.Domain.Entities;
using LibraryAPI.Domain.Repositories;
using LibraryAPI.Domain.Services.Page;
using LibraryAPI.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Infrastructure.Persistance.Repositories
{
    public class BooksRepository : IBooksRepository
    {
        private readonly LibraryContext _dbContext;
        public BooksRepository(LibraryContext context)
        {
            _dbContext = context;
        }
        public IQueryable<Book> GetAll(LibraryQuery query)
        {
            var result = _dbContext.Books
                .WhereIf(!string.IsNullOrEmpty(query.SearchPhrase), b => string.Concat(b.Author, b.Title).Contains(query.SearchPhrase));

            return result;
        }
    }
}
