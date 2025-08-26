using ExamenesApp_Modelos.DTOs;
using ExamenesApp_Modelos.ViewModels;

namespace ExamenesApp_backend.Servicios.IServicios
{
    public interface IServicioPerfilUsuario
    {
        string ObtenerIdUsuario();
        Task<UsuarioDTO> GuardarDatosAsync(UsuarioDTO usuario);
        Task<UsuarioDTO> GetPerfilPorUserIdAsync(string userId);

        Task<bool> DatosExisten();
    }
}
