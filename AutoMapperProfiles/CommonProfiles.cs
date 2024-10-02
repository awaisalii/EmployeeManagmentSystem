using AutoMapper;
using BuisnessLayer.DTO.Request;
using BuisnessLayer.DTO.Response;
using BuisnessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoMapperProfiles
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<ApplicationUser, UserDto>()
                .ForMember(dest=>dest.AssignedTo,
                opt=>opt.MapFrom(src=>src.AssignedTo != null ? src.AssignedTo.FirstName +" "+ src.AssignedTo.LastName:null))
                .ForMember(dest=>dest.AssignedToId,
                opt=>opt.MapFrom(src=>src.AssignedToId));
            CreateMap<TaskModel, TaskDto>() 
                .ForMember(dest => dest.AssignedTo,
                  opt => opt.MapFrom(src => src.AssignedTo != null ? src.AssignedTo.FirstName + " " + src.AssignedTo.LastName : "N/A"))
                .ForMember(dest => dest.GroupChatId,
                   opt => opt.MapFrom(src => src.GroupChat != null
                       ? src.GroupChat.Id
                       : (int?)null)); ;

            CreateMap<Notes, NotesDto>()
                .ForMember(dest => dest.Username,
                    opt => opt.MapFrom(src => src.User != null ? src.User.FirstName + " " + src.User.LastName : "N/A"));
            CreateMap<Notes, NotesRequest>().ReverseMap();
            CreateMap<Activities, ActivitiesRequest>()
                .ForMember(dest => dest.UserName,
                           opt => opt.MapFrom(src => src.User != null ? src.User.FirstName + " " + src.User.LastName : "N/A"));

        }

    }
}
