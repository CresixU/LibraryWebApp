using AutoMapper;
using AutoMapper.QueryableExtensions;
using LibraryAPI.Data.Context;
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
            var roles = await _dbContext
                .Roles
                .Where(r => r.isDeleted == false)
                .ProjectTo<RoleDTO>(_mapper.ConfigurationProvider)
                .OrderByDescending(r => r.IsImmutable)
                .ThenByDescending(r => r.Power)
                .ToListAsync();

            return roles;
        }

        public async Task<int> Create(RoleDTO dto)
        {
            if (dto.Power <= byte.MinValue || dto.Power > byte.MaxValue) return 0;
            var role = _mapper.Map<Role>(dto);

            _dbContext.Roles.Add(role);
            await _dbContext.SaveChangesAsync();

            return role.Id;
        }

        public async Task<bool> Update(int id, RoleDTO dto)
        {
            if (dto.Power <= byte.MinValue || dto.Power > byte.MaxValue) return false;
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
            if (role.IsImmutable) return false;

            // _dbContext.Roles.Remove(role);
            role.isDeleted = true;
            await _dbContext.SaveChangesAsync();
            return true;
        }

    }
}
