using AutoMapper;
using AutoMapper.QueryableExtensions;
using LibraryAPI.Application.Models.Users;
using LibraryAPI.Domain.Services.Page;

namespace LibraryAPI.Application.Services
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
                .WhereIf(!string.IsNullOrEmpty(query.SearchPhrase), u => string.Concat(u.Firstname, u.Lastname, u.Email).Contains(query.SearchPhrase))
                .ProjectTo<UsersDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();

            var users = baseQuery.WithPagination(query);

            var totalItems = baseQuery.Count();

            var result = new PageResult<UsersDTO>(users, totalItems, query.PageSize, query.PageNumber);

            return result;
        }

        public async Task<UserDTO> GetById(int id)
        {
            var user = await _dbContext
                .Users
                .ProjectTo<UserDTO>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(u => u.Id == id);

            return user;
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

            user = _mapper.Map<User>(dto);

            await _dbContext.SaveChangesAsync();

            return true;

        }

        public async Task<bool> Delete(int id)
        {
            var user = await _dbContext
                .Users
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null) return false;

            user.isDeleted = true;
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
