using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTO;
using API.Entities;
using AutoMapper;

namespace API.Helper
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<AppUser, MembersDTO>().ForMember(dest => dest.PhotoURL ,opt => opt.MapFrom(src => src.Photos.FirstOrDefault(x => x.IsMain).URL))
                      .ForMember(dest => dest.Photos, opt => opt.MapFrom(src => src.Photos))  
                      .ForMember(dest =>dest.Age, opt => opt.MapFrom(src => src.DateOfBirth.CalculateAge()));
            CreateMap<Photos,PhotoDTO>(); // Map ObjectId to string
            CreateMap<MemberUpdatedDTO, AppUser>();
            
        }
    }
}