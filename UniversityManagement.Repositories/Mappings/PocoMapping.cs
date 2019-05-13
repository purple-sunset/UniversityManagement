using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using UniversityManagement.Entities.ViewModels;
using UniversityManagement.Repositories.Poco;

namespace UniversityManagement.Repositories.Mappings
{
    public class PocoMapping : Profile
    {
        public PocoMapping()
        {
            CreateMap<AccessToken, AccessTokenViewModel>();
            CreateMap<AccessTokenViewModel, AccessToken>();
        }
    }
}
