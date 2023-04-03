using AutoMapper;
using LibraryAPI.Entities;
using LibraryAPI.Models.Books;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Services
{
    public interface IBookService
    {
        IEnumerable<BookDTO> GetAll();
        BookDTO Get(int id);
        int Create(BookCreateDTO dto);
        bool Update(int id, BookUpdateDTO dto);
        bool Delete(int id);
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

        public IEnumerable<BookDTO> GetAll()
        {
            var books = _dbContext
                .Books
                .Include(b => b.Category)
                .ToList();

            var booksDto = _mapper.Map<List<BookDTO>>(books);

            return booksDto;
        }

        public BookDTO Get(int id)
        {
            var book = _dbContext
                .Books
                .Include(b => b.Category)
                .FirstOrDefault(b => b.Id == id);

            var bookDto = _mapper.Map<BookDTO>(book);

            return bookDto;
        }

        public int Create(BookCreateDTO dto)
        {
            var newBook = _mapper.Map<Book>(dto);
            _dbContext.Books.Add(newBook);
            _dbContext.SaveChanges();

            return newBook.Id;
        }

        public bool Update(int id, BookUpdateDTO dto)
        {
            var oldBook = _dbContext
                .Books
                .Include(b => b.Category)
                .FirstOrDefault(b => b.Id == id);

            if (oldBook is null) return false;

            oldBook.Title = dto.Title;
            oldBook.Author = dto.Author;
            oldBook.PublicationYear = dto.PublicationYear;
            oldBook.CategoryId = dto.CategoryId;

            _dbContext.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            var book =_dbContext
                .Books
                .FirstOrDefault(b => b.Id == id);

            if (book is null) return false;

            _dbContext.Books.Remove(book);
            _dbContext.SaveChanges();
            return true;
        }
    }
}
