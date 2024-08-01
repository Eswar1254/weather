using Microsoft.AspNetCore.Identity;
using MongoDB.Driver;
using weather.Data;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace weather.Services
{
    public class MongoRoleStore<TRole> : IRoleStore<TRole>
        where TRole : IdentityRole
    {
        private readonly IMongoCollection<TRole> _rolesCollection;

        public MongoRoleStore(IMongoDatabase mongoDatabase)
        {
            _rolesCollection = mongoDatabase.GetCollection<TRole>("Roles");
        }

        public Task<IdentityResult> CreateAsync(TRole role, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                _rolesCollection.InsertOne(role);
                return IdentityResult.Success;
            }, cancellationToken);
        }

        public Task<IdentityResult> DeleteAsync(TRole role, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                _rolesCollection.DeleteOne(r => r.Id == role.Id);
                return IdentityResult.Success;
            }, cancellationToken);
        }

        public void Dispose() { }

        public Task<TRole> FindByIdAsync(string roleId, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                return _rolesCollection.Find(r => r.Id == roleId).FirstOrDefault();
            }, cancellationToken);
        }

        public Task<TRole> FindByNameAsync(string roleName, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                return _rolesCollection.Find(r => r.Name == roleName).FirstOrDefault();
            }, cancellationToken);
        }

        public Task<string> GetNormalizedRoleNameAsync(TRole role, CancellationToken cancellationToken)
        {
            return Task.FromResult(role.NormalizedName);
        }

        public Task<string> GetRoleIdAsync(TRole role, CancellationToken cancellationToken)
        {
            return Task.FromResult(role.Id);
        }

        public Task<string> GetRoleNameAsync(TRole role, CancellationToken cancellationToken)
        {
            return Task.FromResult(role.Name);
        }

        public Task SetNormalizedRoleNameAsync(TRole role, string normalizedName, CancellationToken cancellationToken)
        {
            role.NormalizedName = normalizedName;
            return Task.CompletedTask;
        }

        public Task SetRoleNameAsync(TRole role, string roleName, CancellationToken cancellationToken)
        {
            role.Name = roleName;
            return Task.CompletedTask;
        }

        public Task<IdentityResult> UpdateAsync(TRole role, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                _rolesCollection.ReplaceOne(r => r.Id == role.Id, role);
                return IdentityResult.Success;
            }, cancellationToken);
        }
    }
}