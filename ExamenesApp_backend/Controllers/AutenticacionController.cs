using ExamenesApp_backend.Servicios.IServicios;
using ExamenesApp_Modelos.DTOs;
using ExamenesApp_Modelos.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ExamenesApp_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutenticacionController : ControllerBase
    {
        private readonly IServicioAutenticacion _servicioAutenticacion;

        public AutenticacionController(IServicioAutenticacion servicioAutenticacion)
        {
            _servicioAutenticacion = servicioAutenticacion;
        }

        [HttpPost("registro")]
        public async Task<ActionResult<LoginResponseDTO>> Registro([FromBody] RegistroUsuarioViewModel modelo)
        {
            var usuario = await _servicioAutenticacion.Registro(modelo);
            if (!usuario.Exitoso) {

                return BadRequest(usuario);
            }
            return Ok(usuario);


        }

        //[HttpPost("login")]
        //public async Task<ActionResult> Login([FromBody] LoginUsuarioViewModel modelo)
        //{
        //    var resultado = await _servicioAutenticacion.Login(modelo);

        //    // servicio devuelve un string 
        //    if (resultado is null)
        //    {
        //        return Ok(new LoginResponseDTO { Token = null, Mensaje = "Credenciales inválidas" });
        //    }
        //    return Ok(resultado);
        //}
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUsuarioViewModel userDTO)
        {
            var token = await _servicioAutenticacion.Login(userDTO);

            if (token is null)
            {
                return Unauthorized(new LoginResponseDTO
                {
                    Token = null,
                    Mensaje = "Correo o contraseña incorrectos"
                });
            }

           

            return Ok(new LoginResponseDTO
            {
                Token = token,
                Mensaje = "Login exitoso"
            });
        }

    }
}
