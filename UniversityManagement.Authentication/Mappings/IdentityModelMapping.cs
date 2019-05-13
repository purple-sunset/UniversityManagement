using AutoMapper;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityManagement.Entities.ViewModels;

namespace UniversityManagement.Authentication.Mappings
{
    public class IdentityModelMapping : Profile
    {
        public IdentityModelMapping()
        {
            CreateMap<PersistedGrant, AccessTokenViewModel>();
            CreateMap<AccessTokenViewModel, PersistedGrant>();
        }
    }
}
