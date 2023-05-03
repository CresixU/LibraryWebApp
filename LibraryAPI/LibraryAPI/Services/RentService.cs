using AutoMapper;
using LibraryAPI.Entities;
using LibraryAPI.Exceptions;
using LibraryAPI.Models;
using LibraryAPI.Models.Books;
using LibraryAPI.Models.Rents;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Services
{
    public interface IRentService
    {
        Task<PageResult<RentDTO>> GetAll(LibraryQuery query);
        Task<PageResult<RentDTO>> GetAllByUserId(int id, LibraryQuery query);
        Task<int> RentBooks(RentCreateDTO dto);
        Task<bool> ReturnBooks(int rentId, RentReturnDTO dto);
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

        public async Task<PageResult<RentDTO>> GetAll(LibraryQuery query)
        {
            var baseQuery = await _dbContext
                        .Rents
                        .Include(r => r.User)
                        .Include(r => r.Books)
                        .Where(r => query.SearchPhrase == null || ((r.User.Firstname + ' ' + r.User.Lastname).ToLower().Contains(query.SearchPhrase.ToLower())
                                                                || r.User.Email.ToLower().Contains(query.SearchPhrase.ToLower())))
                        .ToListAsync();

            var rents = baseQuery
                .Skip(query.PageSize * (query.PageNumber - 1))
                .Take(query.PageSize)
                .ToList();

            var totalItems = baseQuery.Count();

            var dtos = _mapper.Map<List<RentDTO>>(rents);

            var result = new PageResult<RentDTO>(dtos, totalItems, query.PageSize, query.PageNumber);

            return result;
        }

        public async Task<PageResult<RentDTO>> GetAllByUserId(int id, LibraryQuery query)
        {
            var user = await _dbContext
                        .Users
                        .Include(u => u.Rents)
                        .ThenInclude(r => r.Books)
                        .ThenInclude(b => b.Category)
                        .FirstOrDefaultAsync(u => u.Id == id);
            if (user is null)
                throw new NotFoundException("User not found");

            var rents = user.Rents
                            .Skip(query.PageSize * (query.PageNumber - 1))
                            .Take(query.PageSize)
                            .ToList();

            var totalItems = user.Rents.Count();

            var dtos = _mapper.Map<List<RentDTO>>(user.Rents);

            var result = new PageResult<RentDTO>(dtos, totalItems, query.PageSize, query.PageNumber);

            return result;
        }

        public async Task<int> RentBooks(RentCreateDTO dto)
        {
            var books = await _dbContext
                        .Books
                        .Where(b => dto.BookIds.Contains(b.Id))
                        .ToListAsync();

            if (books.Count == 0)
                throw new NotFoundException("Book not found");
            books.ForEach(b => b.IsAvailable = false);

            var rent = _mapper.Map<Rent>(dto);
            rent.Books = books;

            _dbContext.Rents.Add(rent);
            await _dbContext.SaveChangesAsync();

            return rent.Id;

        }

        public async Task<bool> ReturnBooks(int id, RentReturnDTO dto)
        {
            var rent = await _dbContext
                .Rents
                .Include(r => r.Books)
                .FirstOrDefaultAsync(r => r.Id == id);

            if(rent is null) return false;

            rent.ReturnDate = dto.ReturnDate;
            rent.Books.ForEach(b => b.IsAvailable = true);
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
