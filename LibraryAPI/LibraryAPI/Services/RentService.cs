using AutoMapper;
using LibraryAPI.Entities;
using LibraryAPI.Models.Books;
using LibraryAPI.Models.Rents;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Services
{
    public interface IRentService
    {
        IEnumerable<RentDTO> GetAll();
        IEnumerable<RentDTO> GetAllByUserId(int id);
        int RentBooks(List<int> bookIds);
        bool ReturnBooks(int rentId, List<int> bookIds);
    }

    public class RentService : IRentService
    {
        private readonly IMapper _mapper;
        private readonly LibraryContext _dbContext;

        public RentService(IMapper mapper, LibraryContext context)
        {
            _mapper = mapper;
            _dbContext = context;
        }

        public IEnumerable<RentDTO> GetAll()
        {
            var rents = _dbContext
                .Rents
                .Include(r => r.User)
                .Include(r => r.Books)
                .ToList();

            var dtos = _mapper.Map<List<RentDTO>>(rents);

            return dtos;
        }

        public IEnumerable<RentDTO> GetAllByUserId(int id)
        {
            var rents = _dbContext
                        .Users
                        .Include(u => u.Rents)
                        .FirstOrDefault(u => u.Id == id)
                        .Rents;

            var dtos = _mapper.Map<List<RentDTO>>(rents);

            return dtos;
        }

        public int RentBooks(List<int> bookIds)
        {
            var books = _dbContext
                .Books
                .Where(b => bookIds.Contains(b.Id))
                .ToList();

            if (books.Count == 0) return 0;

            var bookDtos = _mapper.Map<List<BookDTO>>(books);

            var rentDto = new RentCreateDTO()
            {
                RentDate = DateTime.Now,
                Books = bookDtos
            };

            var rent = _mapper.Map<Rent>(rentDto);

            _dbContext.Rents.Add(rent);
            _dbContext.SaveChanges();

            return rent.Id;
        }

        public bool ReturnBooks(int rentId, List<int> bookIds)
        {
            var rent = _dbContext
                .Rents
                .FirstOrDefault(r => r.Id == rentId);

            if(rent is null) return false;

            rent.ReturnDate = DateTime.Now;
            _dbContext.SaveChanges();

            return true;
        }
    }
}
