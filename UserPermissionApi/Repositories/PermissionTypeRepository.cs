using Microsoft.EntityFrameworkCore;
using UserPermissionApi.Data;
using UserPermissionApi.Model;

namespace UserPermissionApi.Repositories
{
    public class PermissionTypeRepository : IPermissionTypeRepository<PermissionTypes>
    {
        private readonly UserPermissionDbContext _context;

        public PermissionTypeRepository(UserPermissionDbContext context)
        {
            _context = context;
        }

        public async Task<PermissionTypes?> GetByIdAsync(int id)
        {
            return await _context.PermissionTypes.FindAsync(id);
        }

        public async Task<PermissionTypes?> GetByNameAsync(string name)
        {
            var list = await _context.PermissionTypes.ToListAsync();

            if (list.Any() && list.Where(x => x.Descripcion == name).Any())
            {
                return list.Where(x => x.Descripcion == name).FirstOrDefault();
            }
            else
            {
                return null;
            }
        }

        public async Task<IEnumerable<PermissionTypes>> GetAllAsync()
        {
            return await _context.PermissionTypes.ToListAsync();
        }

        public async Task AddAsync(PermissionTypes permissionType)
        {
            _context.PermissionTypes.Add(permissionType);
            var r = await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(PermissionTypes permissionType)
        {
            _context.PermissionTypes.Update(permissionType);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var permissionType = await _context.PermissionTypes.FindAsync(id);
            if (permissionType != null)
            {
                _context.PermissionTypes.Remove(permissionType);
                await _context.SaveChangesAsync();
            }
        }
    }
}
