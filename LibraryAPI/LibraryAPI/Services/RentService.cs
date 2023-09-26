using AutoMapper;
using AutoMapper.QueryableExtensions;
using LibraryAPI.Data.Context;
using LibraryAPI.Entities;
using LibraryAPI.Exceptions;
using LibraryAPI.Models;
using LibraryAPI.Models.Books;
using LibraryAPI.Models.Rents;
using LibraryAPI.Services.Extensions;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Services
{
    public interface IRentService
    {
        Task<PageResult<RentDTO>> GetAll(LibraryQuery query);
        Task<PageResult<RentDTO>> GetAllByUserId(int id, LibraryQuery query);
        Task<int> RentBook(RentCreateDTO dto);
        Task<bool> ReturnBook(int rentId, RentReturnDTO dto);
        Task<bool> DeleteRent(int rentId);
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
                        .WhereIf(!string.IsNullOrEmpty(query.SearchPhrase), r => string.Concat(r.User.Firstname, r.User.Lastname, r.User.Email).Contains(query.SearchPhrase))
                        .ProjectTo<RentDTO>(_mapper.ConfigurationProvider)
                        .ToListAsync();

            var rents = baseQuery.WithPagination(query);

            var totalItems = baseQuery.Count();

            var result = new PageResult<RentDTO>(rents, totalItems, query.PageSize, query.PageNumber);

            return result;
        }

        public async Task<PageResult<RentDTO>> GetAllByUserId(int id, LibraryQuery query)
        {
            var user = await _dbContext
                        .Users
                        .Include(u => u.Rents)
                        .ThenInclude(r => r.Book)
                        .ThenInclude(b => b.Category)
                        .FirstOrDefaultAsync(u => u.Id == id);
            if (user is null)
                throw new NotFoundException("User not found");

            var rents = user.Rents
                            .Skip(query.PageSize * (query.PageNumber - 1))
                            .Take(query.PageSize)
                            .ToList();

            var totalItems = user.Rents.Count();

            var dtos = _mapper.Map<List<RentDTO>>(rents);

            var result = new PageResult<RentDTO>(dtos, totalItems, query.PageSize, query.PageNumber);

            return result;
        }

        public async Task<int> RentBook(RentCreateDTO dto)
        {
            var book = await _dbContext
                        .Books
                        .FirstOrDefaultAsync(b => dto.BookId.Equals(b.Id));

            if (book is null)
                throw new NotFoundException("Book not found");

            book.IsAvailable = false;

            if (dto.RentDate is null) dto.RentDate = DateTime.Now;

            var rent = _mapper.Map<Rent>(dto);
            rent.Book = book;

            _dbContext.Rents.Add(rent);
            await _dbContext.SaveChangesAsync();

            return rent.Id;

        }

        public async Task<bool> ReturnBook(int id, RentReturnDTO dto)
        {
            var rent = await _dbContext
                .Rents
                .Include(r => r.Book)
                .FirstOrDefaultAsync(r => r.Id == id);

            if(rent is null) return false;

            rent.ReturnDate = dto.ReturnDate;
            rent.Book.IsAvailable = true;
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteRent(int rentId)
        {
            var rent = await _dbContext
                .Rents
                .FirstOrDefaultAsync(r => r.Id == rentId);

            if (rent is null) return false;
            if (rent.ReturnDate is null) return false;

            rent.isDeleted = true;
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
