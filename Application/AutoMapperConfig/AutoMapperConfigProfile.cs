using Application.DTOs.Response;
using AutoMapper;
using Domain.Entities;

namespace Application.AutoMapper
{
    public class AutoMapperConfigProfile : Profile
    {
        public AutoMapperConfigProfile()
        {
            CreateMap<Usuario, UsuarioResponse>();
            CreateMap<Lojista, LojistaResponse>();
        }
    }
}
