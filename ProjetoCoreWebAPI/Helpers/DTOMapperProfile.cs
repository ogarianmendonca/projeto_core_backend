using AutoMapper;
using ProjetoCoreWebAPI.DTO;
using ProjetoCoreWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoCoreWebAPI.Helpers
{
    public class DTOMapperProfile : Profile
    {
        public DTOMapperProfile()
        {
            CreateMap<User, UsuarioCreateDTO>().ReverseMap();
            CreateMap<User, UsuarioLoginDTO>().ReverseMap();
            CreateMap<User, UsuarioDTO>().ReverseMap();
            CreateMap<Role, RoleDTO>().ReverseMap();
        }
    }
}
