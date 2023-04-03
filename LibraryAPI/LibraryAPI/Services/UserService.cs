using AutoMapper;
using LibraryAPI.Entities;
using LibraryAPI.Models.Users;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Services
{
    public interface IUserService
    {
        IEnumerable<UsersDTO> GetAll();
        UserDTO GetById(int id);
        int Create(UserCreateDTO dto);
        bool Update(int id, UserUpdateDTO dto);
        bool Delete(int id);

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

        public IEnumerable<UsersDTO> GetAll()
        {
            var users = _dbContext
                .Users
                .Include(u => u.Address)
                .ToList();

            var usersDtos = _mapper.Map<List<UsersDTO>>(users);

            return usersDtos;
        }

        public UserDTO GetById(int id)
        {
            var user = _dbContext
                .Users
                .Include(u => u.Address)
                .Include(u => u.Role)
                .Include(u => u.Rents)
                .FirstOrDefault(u => u.Id == id);

            var userDto = _mapper.Map<UserDTO>(user);

            return userDto;
        }

        public int Create(UserCreateDTO dto)
        {
            var user = _mapper.Map<User>(dto);
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();

            return user.Id;
        }

        public bool Update(int id, UserUpdateDTO dto)
        {
            var user = _dbContext
                .Users
                .Include(u => u.Address)
                .FirstOrDefault(u => u.Id == id);

            if (user is null) return false; 

            user.Firstname = dto.Firstname;
            user.Lastname = dto.Lastname;
            user.Email = dto.Email;
            user.Address.PostalCode = dto.PostalCode;
            user.Address.City = dto.City;
            user.Address.Street = dto.Street;
            user.Address.Number = dto.Number;

            _dbContext.SaveChanges();

            return true;

        }

        public bool Delete(int id)
        {
            var user = _dbContext
                .Users
                .FirstOrDefault(u => u.Id == id);

            if (user == null) return false;

            _dbContext.Users.Remove(user);
            _dbContext.SaveChanges();

            return true;
        }
    }
}
