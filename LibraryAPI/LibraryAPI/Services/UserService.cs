using AutoMapper;
using LibraryAPI.Entities;
using LibraryAPI.Models;
using LibraryAPI.Models.Users;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Services
{
    public interface IUserService
    {
        Task<PageResult<UsersDTO>> GetAll(LibraryQuery query);
        Task<UserDTO> GetById(int id);
        Task<int> Create(UserCreateDTO dto);
        Task<bool> Update(int id, UserUpdateDTO dto);
        Task<bool> Delete(int id);
        Task<bool> RoleUpdate(int userId, int roleId);

    }

    public class UserService : IUserService
    {
        private readonly LibraryContext _dbContext;
        private readonly IMapper _mapper;

        public UserService(LibraryContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<PageResult<UsersDTO>> GetAll(LibraryQuery query)
        {
            var baseQuery = await _dbContext
                .Users
                .Include(u => u.Address)
                .Where(u => query.SearchPhrase == null || ((u.Firstname + ' ' + u.Lastname).ToLower().Contains(query.SearchPhrase.ToLower())
                                                                || u.Email.ToLower().Contains(query.SearchPhrase.ToLower())))
                .ToListAsync();

            var users = baseQuery
                .Skip(query.PageSize * (query.PageNumber - 1))
                .Take(query.PageSize)
                .ToList();

            var totalItems = baseQuery.Count();

            var usersDtos = _mapper.Map<List<UsersDTO>>(users);

            var result = new PageResult<UsersDTO>(usersDtos, totalItems, query.PageSize, query.PageNumber);

            return result;
        }

        public async Task<UserDTO> GetById(int id)
        {
            var user = await _dbContext
                .Users
                .Include(u => u.Address)
                .Include(u => u.Role)
                .Include(u => u.Rents)
                .FirstOrDefaultAsync(u => u.Id == id);

            var userDto = _mapper.Map<UserDTO>(user);

            return userDto;
        }

        public async Task<int> Create(UserCreateDTO dto)
        {
            var user = _mapper.Map<User>(dto);
            var role = await _dbContext.Roles.FirstOrDefaultAsync(r => r.Name == "User");
            user.Role = role;
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();

            return user.Id;
        }

        public async Task<bool> Update(int id, UserUpdateDTO dto)
        {
            var user = await _dbContext
                .Users
                .Include(u => u.Address)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user is null) return false; 

            user.Firstname = dto.Firstname;
            user.Lastname = dto.Lastname;
            user.Email = dto.Email;
            user.Address.PostalCode = dto.PostalCode;
            user.Address.City = dto.City;
            user.Address.Street = dto.Street;
            user.Address.Number = dto.Number;

            await _dbContext.SaveChangesAsync();

            return true;

        }

        public async Task<bool> Delete(int id)
        {
            var user = await _dbContext
                .Users
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null) return false;

            _dbContext.Users.Remove(user);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> RoleUpdate(int userId, int roleId)
        {
            var user = await _dbContext
                .Users
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user is null) return false;

            var role = await _dbContext
                .Roles
                .FirstOrDefaultAsync(r => r.Id == roleId);

            if (role == null) return false;

            user.Role = role;
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
