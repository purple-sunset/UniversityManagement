using System;
using System.Collections.Generic;
using System.Text;
using UniversityManagement.Entities.ViewModels;
using UniversityManagement.Repositories.Implements;
using UniversityManagement.Repositories.Poco;

namespace UniversityManagement.Services.Implements
{
    public interface IAccessTokenService: IBaseService<AccessToken, AccessTokenViewModel>
    {
    }
    public class AccessTokenService : BaseService<AccessToken, AccessTokenViewModel>, IAccessTokenService
    {
        public AccessTokenService(IUnitOfWork unitOfWork, IBaseRepository<AccessToken> repository) : base(unitOfWork, repository)
        {
        }
    }
}
