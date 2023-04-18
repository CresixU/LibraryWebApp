using AutoMapper;
using LibraryAPI.Entities;
using LibraryAPI.Models.Roles;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Services
{
    public interface IRoleService
    {
        Task<IEnumerable<RoleDTO>> GetAll();
        Task<int> Create(RoleDTO dto);
        Task<bool> Update(int id, RoleDTO dto);
        Task<bool> Delete(int id);
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

        public async Task<IEnumerable<RoleDTO>> GetAll()
        {
            var roles = await _dbContext.Roles.ToListAsync();

            var dtos = _mapper.Map<List<RoleDTO>>(roles);

            return dtos;
        }

        public async Task<int> Create(RoleDTO dto)
        {
            var role = _mapper.Map<Role>(dto);

            _dbContext.Roles.Add(role);
            await _dbContext.SaveChangesAsync();

            return role.Id;
        }

        public async Task<bool> Update(int id, RoleDTO dto)
        {
            var role = await _dbContext
                .Roles
                .FirstOrDefaultAsync(r => r.Id == id);

            if (role is null) return false;

            role.Name = dto.Name;
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Delete(int id)
        {
            var role = await _dbContext
                .Roles
                .FirstOrDefaultAsync(r => r.Id == id);

            if (role is null) return false;

            _dbContext.Roles.Remove(role);
            await _dbContext.SaveChangesAsync();
            return true;
        }

    }
}
