using Microsoft.EntityFrameworkCore;
using UserPermissionApi.Data;
using UserPermissionApi.Model;

namespace UserPermissionApi.Repositories
{
    public class PermissionRepository : IPermissionRepository
    {
        private readonly UserPermissionDbContext _context;

        public PermissionRepository(UserPermissionDbContext context)
        {
            _context = context;
        }

        public async Task<Permissions> GetByIdAsync(int id)
        {
            return await _context.Permissions.Include(p => p.PermissionTypes).Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Permissions>> GetAllAsync()
        {
            return await _context.Permissions.Include(p => p.PermissionTypes).ToListAsync();
        }

        public async Task AddAsync(Permissions permission)
        {
            _context.Permissions.Add(permission);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Permissions permission)
        {
            _context.Permissions.Update(permission);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var permission = await _context.Permissions.FindAsync(id);
            if (permission != null)
            {
                _context.Permissions.Remove(permission);
                await _context.SaveChangesAsync();
            }
        }
    }
}
