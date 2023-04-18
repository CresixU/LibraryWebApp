using AutoMapper;
using LibraryAPI.Entities;
using LibraryAPI.Models.Users;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UsersDTO>> GetAll();
        Task<UserDTO> GetById(int id);
        Task<int> Create(UserCreateDTO dto);
        Task<bool> Update(int id, UserUpdateDTO dto);
        Task<bool> Delete(int id);

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

        public async Task<IEnumerable<UsersDTO>> GetAll()
        {
            var users = await _dbContext
                .Users
                .Include(u => u.Address)
                .ToListAsync();

            var usersDtos = _mapper.Map<List<UsersDTO>>(users);

            return usersDtos;
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
    }
}
