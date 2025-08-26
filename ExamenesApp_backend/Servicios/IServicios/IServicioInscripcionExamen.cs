using ExamenesApp_Modelos.DTOs;

namespace ExamenesApp_backend.Servicios.IServicios
{
    public interface IServicioInscripcionExamen
    {
        Task<InscripcionExamenDTO> RegistarInscripcionExamenes(int examenId);
        //string GenerarCodigo();
        //Task<DateTime> GenerarFechaAleatoria();
        //DateTime ObtenerFechaLimiteCancelacion();
        //Task<DateTime?> Fecha(string examen);
        Task<IEnumerable<InscripcionExamenDTO>> ObtenerInscripcionesUsuarioAsync();
        Task<bool> EliminarInscripcionExamen(int id);
    }
}
