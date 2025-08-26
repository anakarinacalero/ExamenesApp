using ExamenesApp_backend.Servicios.IServicios;
using ExamenesApp_Modelos.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExamenesApp_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // Solo usuarios autenticados pueden acceder
    public class InscripcionExamenesController : ControllerBase
    {
        private readonly IServicioInscripcionExamen _servicioInscripcion;

        public InscripcionExamenesController(IServicioInscripcionExamen servicioInscripcion)
        {
            _servicioInscripcion = servicioInscripcion;
        }

        
        [HttpGet("inscripciones")]
        public async Task<ActionResult<IEnumerable<InscripcionExamenDTO>>> ObtenerInscripciones()
        {
            var inscripciones = await _servicioInscripcion.ObtenerInscripcionesUsuarioAsync();
            if (inscripciones == null || !inscripciones.Any())
            {
                return NotFound("No se encontraron inscripciones para el usuario.");
            }

            return Ok(inscripciones); 
             
        }

        [HttpPost("inscripcion")]
        public async Task<ActionResult<InscripcionExamenDTO>> RegistrarInscripcion([FromBody] int examenId)
        {
                var inscripcion = await _servicioInscripcion.RegistarInscripcionExamenes(examenId);
                if (inscripcion == null)
                {
                    return BadRequest(new { mensaje = "Ya existe una inscripción para este examen" });
                }
                return Ok(inscripcion);
            }
            
        [HttpDelete("eliminar/{id}")]
        public async Task<bool> EliminarInscripcion(int id)
        {
                var resultado = await _servicioInscripcion.EliminarInscripcionExamen(id);
                if (!resultado)
                    return false;

                return true;

        }
    }
}
