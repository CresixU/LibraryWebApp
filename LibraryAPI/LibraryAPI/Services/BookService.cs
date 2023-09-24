using AutoMapper;
using AutoMapper.QueryableExtensions;
using LibraryAPI.Data.Context;
using LibraryAPI.Entities;
using LibraryAPI.Models;
using LibraryAPI.Models.Books;
using LibraryAPI.Services.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace LibraryAPI.Services
{
    public interface IBookService
    {
        Task<PageResult<BookDTO>> GetAll(LibraryQuery query);
        Task<BookDTO> Get(int id);
        Task<int> Create(BookCreateDTO dto);
        Task<bool> Update(int id, BookUpdateDTO dto);
        Task<bool> Delete(int id);
    } 

    public class BookService : IBookService
    {
        private readonly LibraryContext _dbContext;
        private readonly IMapper _mapper;

        public BookService(LibraryContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<PageResult<BookDTO>> GetAll(LibraryQuery query)
        {
            var baseQuery = await _dbContext
                .Books
                .WhereIf(!string.IsNullOrEmpty(query.SearchPhrase), b => string.Concat(b.Author, b.Title).Contains(query.SearchPhrase))
                .ProjectTo<BookDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();

            var books = baseQuery.WithPagination(query);

            var totalItems = baseQuery.Count();

            var result = new PageResult<BookDTO>(books, totalItems, query.PageSize, query.PageNumber);

            return result;
        }

        public async Task<BookDTO> Get(int id)
        {
            var book = await _dbContext
                .Books
                .ProjectTo<BookDTO>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(b => b.Id == id);

            var bookDto = _mapper.Map<BookDTO>(book);

            return bookDto;
        }

        public async Task<int> Create(BookCreateDTO dto)
        {
            var newBook = _mapper.Map<Book>(dto);
            await _dbContext.Books.AddAsync(newBook);
            await _dbContext.SaveChangesAsync();

            return newBook.Id;
        }

        public async Task<bool> Update(int id, BookUpdateDTO dto)
        {
            var oldBook = await _dbContext
                .Books
                .Include(b => b.Category)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (oldBook is null) return false;

            oldBook.Title = dto.Title;
            oldBook.Author = dto.Author;
            oldBook.PublicationYear = dto.PublicationYear;
            oldBook.CategoryId = dto.CategoryId;

            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(int id)
        {
            var book = await _dbContext
                .Books
                .FirstOrDefaultAsync(b => b.Id == id);

            if (book is null) return false;

            book.isDeleted = true;
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
