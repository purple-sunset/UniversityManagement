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
            //CreateMap<BaseEntity, BaseViewModel>(MemberList.Destination)
            //    .IncludeAllDerived();

            CreateMap<BaseViewModel, BaseEntity>()
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
                .Include<AccessTokenViewModel, AccessToken>()
                .ReverseMap();
                
                //.IncludeAllDerived();

            //CreateMap<AccessToken, AccessTokenViewModel>();
            CreateMap<AccessTokenViewModel, AccessToken>()
                .ReverseMap();
        }
    }
}
