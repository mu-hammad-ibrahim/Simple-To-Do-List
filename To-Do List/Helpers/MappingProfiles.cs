using AutoMapper;
using To_Do_List.Dto;
using To_Do_List.DAL.Models;
using System.Net;

namespace To_Do_List.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Tasks, TaskDto>()
                .ReverseMap();
        }
    }
}
