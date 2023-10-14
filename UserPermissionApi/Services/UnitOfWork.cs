using Microsoft.EntityFrameworkCore.Storage;
using UserPermissionApi.Data;
using UserPermissionApi.Repositories;

namespace UserPermissionApi.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly UserPermissionDbContext _context;
        private IDbContextTransaction _transaction;

        public IPermissionRepository Permissions { get; }
        public IPermissionTypeRepository PermissionTypes { get; }

        public UnitOfWork(UserPermissionDbContext context)
        {
            _context = context;
            Permissions = new PermissionRepository(_context);
            PermissionTypes = new PermissionTypeRepository(_context);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
        public void BeginTransaction()
        {
            _transaction = _context.Database.BeginTransaction();
        }

        public void Commit()
        {
            try
            {
                _context.SaveChanges();
                _transaction.Commit();
            }
            catch
            {
                // Manejar excepciones de transacción, como una validación fallida
                _transaction.Rollback();
                throw; // Lanzar la excepción nuevamente para que la capa superior pueda manejarla si es necesario
            }
        }

        public void Rollback()
        {
            _transaction.Rollback();
        }

        public void Dispose()
        {
            _transaction?.Dispose();
        }
    }

}
