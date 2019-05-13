using AutoMapper;
using AutoMapper.QueryableExtensions;
using IdentityServer4.Models;
using IdentityServer4.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityManagement.Entities.ViewModels;
using UniversityManagement.Services.Implements;

namespace UniversityManagement.Authentication.Stores
{
    public class PersistedGrantStore : IPersistedGrantStore
    {
        protected IAccessTokenService AccessTokenService { get; }
        public PersistedGrantStore(IAccessTokenService accessTokenService)
        {
            AccessTokenService = accessTokenService;
        }
        public async Task<IEnumerable<PersistedGrant>> GetAllAsync(string subjectId)
        {
            return AccessTokenService.GetAll(x => x.SubjectId == subjectId).ProjectTo<PersistedGrant>();
        }

        public async Task<PersistedGrant> GetAsync(string key)
        {
            var accessToken = AccessTokenService.GetModelBy(x => x.Key == key);
            return Mapper.Map<AccessTokenViewModel, PersistedGrant>(accessToken);
        }

        public async Task RemoveAllAsync(string subjectId, string clientId)
        {
            await AccessTokenService.HardDeleteByAsync(e => e.SubjectId == subjectId && e.ClientId == clientId);
        }

        public async Task RemoveAllAsync(string subjectId, string clientId, string type)
        {
            await AccessTokenService.HardDeleteByAsync(e => e.SubjectId == subjectId && e.ClientId == clientId && e.Type == type );
        }

        public async Task RemoveAsync(string key)
        {
            await AccessTokenService.HardDeleteByAsync(e => e.Key == key);
        }

        public async Task StoreAsync(PersistedGrant grant)
        {
            var accessToken = Mapper.Map<AccessTokenViewModel>(grant);
            await AccessTokenService.AddAsync(accessToken);
        }
    }
}
