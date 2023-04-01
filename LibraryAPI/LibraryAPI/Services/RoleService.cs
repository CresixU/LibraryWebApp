using AutoMapper;
using LibraryAPI.Entities;
using LibraryAPI.Models.Role;

namespace LibraryAPI.Services
{
    public interface IRoleService
    {
        IEnumerable<RoleDTO> GetAll();
        int Create(RoleDTO dto);
        bool Update(int id, RoleDTO dto);
        bool Delete(int id);
    }

    public class RoleService : IRoleService
    {
        private readonly LibraryContext _dbContext;
        private readonly IMapper _mapper;

        public RoleService(LibraryContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public IEnumerable<RoleDTO> GetAll()
        {
            var roles = _dbContext.Roles.ToList();

            var dtos = _mapper.Map<List<RoleDTO>>(roles);

            return dtos;
        }

        public int Create(RoleDTO dto)
        {
            var role = _mapper.Map<Role>(dto);

            _dbContext.Roles.Add(role);
            _dbContext.SaveChanges();

            return role.Id;
        }

        public bool Update(int id, RoleDTO dto)
        {
            var role = _dbContext
                .Roles
                .FirstOrDefault(r => r.Id == id);

            if (role is null) return false;

            role.Name = dto.Name;
            _dbContext.SaveChanges();

            return true;
        }

        public bool Delete(int id)
        {
            var role = _dbContext
                .Roles
                .FirstOrDefault(r => r.Id == id);

            if (role is null) return false;

            _dbContext.Roles.Remove(role);
            _dbContext.SaveChanges();
            return true;
        }

    }
}
