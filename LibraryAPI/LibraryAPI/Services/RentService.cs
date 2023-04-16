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
        int RentBooks(RentCreateDTO dto);
        bool ReturnBooks(int rentId, RentReturnDTO dto);
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
            var user = _dbContext
                        .Users
                        .Include(u => u.Rents)
                        .ThenInclude(r => r.Books)
                        .ThenInclude(b => b.Category)
                        .FirstOrDefault(u => u.Id == id);
            if (user is null) return null;

            var dtos = _mapper.Map<List<RentDTO>>(user.Rents);

            return dtos;
        }

        public int RentBooks(RentCreateDTO dto)
        {
            var books = _dbContext
                        .Books
                        .Where(b => dto.BookIds.Contains(b.Id))
                        .ToList();

            if (books.Count == 0) return 0;
            books.ForEach(b => b.IsAvailable = false);

            var rent = _mapper.Map<Rent>(dto);
            rent.Books = books;

            _dbContext.Rents.Add(rent);
            _dbContext.SaveChanges();

            return rent.Id;

        }

        public bool ReturnBooks(int id, RentReturnDTO dto)
        {
            var rent = _dbContext
                .Rents
                .Include(r => r.Books)
                .FirstOrDefault(r => r.Id == id);

            if(rent is null) return false;

            rent.ReturnDate = dto.ReturnDate;
            rent.Books.ForEach(b => b.IsAvailable = true);
            _dbContext.SaveChanges();

            return true;
        }
    }
}
