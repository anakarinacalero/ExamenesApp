using ExamenesApp_Modelos;
using ExamenesApp_Modelos.DTOs;
using AutoMapper;
namespace ExamenesApp_backend.Mapping
{
    public class UsuarioProfile:Profile
    {
        public UsuarioProfile()
        {
            CreateMap<UsuarioDTO, Usuario>()
                .ReverseMap();

            CreateMap<InscripcionExamenDTO, Inscripcion>()
               .ReverseMap();
        }

    }
}
