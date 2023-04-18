using AutoMapper;
using LibraryAPI.Entities;
using LibraryAPI.Models.Books;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Services
{
    public interface IBookService
    {
        Task<IEnumerable<BookDTO>> GetAll();
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

        public async Task<IEnumerable<BookDTO>> GetAll()
        {
            var books = await _dbContext
                .Books
                .Include(b => b.Category)
                .ToListAsync();

            var booksDto = _mapper.Map<List<BookDTO>>(books);

            return booksDto;
        }

        public async Task<BookDTO> Get(int id)
        {
            var book = await _dbContext
                .Books
                .Include(b => b.Category)
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

            _dbContext.Books.Remove(book);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
