using ExamenesApp_backend.Servicios;
using ExamenesApp_backend.Servicios.IServicios;
using ExamenesApp_Modelos.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExamenesApp_backend.Controllers
{
   
        [Route("api/[controller]")]
        [ApiController]
        [Authorize] // asegura que solo usuarios autenticados accedan
        public class PerfilUsuarioController : ControllerBase
        {
            private readonly IServicioPerfilUsuario servicioPerfilUsuario;

            public PerfilUsuarioController(IServicioPerfilUsuario servicioPerfilUsuario)
            {
                this.servicioPerfilUsuario = servicioPerfilUsuario;
            }

            // GET: api/PerfilUsuario
            [HttpGet("mi-perfil")]
            public async Task<ActionResult<UsuarioDTO>> GetMiPerfil()
            {
                var userId = servicioPerfilUsuario.ObtenerIdUsuario();
                if (userId == null)
                {
                    return Unauthorized("Usuario no autenticado.");
                }

                var perfil = await servicioPerfilUsuario.GetPerfilPorUserIdAsync(userId);
                if (perfil == null)
                {
                    return NotFound("El perfil no existe.");
                }

                return Ok(perfil);
            }

            // POST: api/PerfilUsuario
            [HttpPost("guardar")]
            public async Task<ActionResult<UsuarioDTO>> GuardarPerfil([FromBody] UsuarioDTO usuario)
            {
                if (usuario == null)
                {
                    return BadRequest("Datos inválidos.");
                }

                var perfilGuardado = await servicioPerfilUsuario.GuardarDatosAsync(usuario);
                return Ok(perfilGuardado);
            }
          
            [HttpGet("perfil")]
            public async Task<ActionResult> ObtenerPerfil()
            {

                var userId = servicioPerfilUsuario.ObtenerIdUsuario();
                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized("Usuario no autenticado.");
                }
            

                var perfil = await servicioPerfilUsuario.GetPerfilPorUserIdAsync(userId);

                if (perfil == null)
                    return NotFound("No se encontró el usuario.");

                return Ok(perfil);
            }
        }
    }



