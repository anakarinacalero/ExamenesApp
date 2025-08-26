using ExamenesApp_backend.Data;
using ExamenesApp_Modelos;
using ExamenesApp_Modelos.DTOs;
using ExamenesApp_Modelos.ViewModels;

namespace ExamenesApp_backend.Servicios.IServicios
{
    public interface IServicioAutenticacion
    {
        Task<string> Login(LoginUsuarioViewModel userDTO);

        Task<RegistroResponse> Registro(RegistroUsuarioViewModel modelo);

    }
}
