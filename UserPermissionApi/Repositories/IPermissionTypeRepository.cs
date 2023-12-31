﻿using UserPermissionApi.Model;

namespace UserPermissionApi.Repositories
{
    public interface IPermissionTypeRepository<T> where T : class
    {
        Task<PermissionTypes?> GetByIdAsync(int id);
        Task<PermissionTypes?> GetByNameAsync(string name);
        Task<IEnumerable<PermissionTypes>> GetAllAsync();
        Task AddAsync(PermissionTypes permission);
        Task UpdateAsync(PermissionTypes permission);
        Task DeleteAsync(int id);
    }
}
