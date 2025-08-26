using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ExamenesApp_backend.Data;
using ExamenesApp_backend.Servicios.IServicios;
using ExamenesApp_Modelos;
using ExamenesApp_Modelos.DTOs;
using ExamenesApp_Modelos.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ExamenesApp_backend.Servicios
{
    public class ServicioAutenticacion : IServicioAutenticacion
    {
        private readonly IConfiguration configuration;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ApplicationDbContext context;

        public ServicioAutenticacion(IConfiguration configuration,
            UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            this.configuration = configuration;
            this.userManager = userManager;
            this.context = context;
        }
        public async Task<string> Login(LoginUsuarioViewModel userDTO )
        {
            var usuario = await userManager.FindByEmailAsync(userDTO.Email);
            if (usuario == null || !await userManager.CheckPasswordAsync(usuario, userDTO.Password))
                return null;

            var token = CreateToken(usuario);
            return token;

        }

        //public async Task<RegistroResponse> Registro(RegistroUsuarioViewModel modelo)
        //{
        //    var mensaje=string.Empty;
        //    var existeUsuario = await userManager.FindByEmailAsync(modelo.Email);

        //    if (existeUsuario != null)
        //    {
        //        mensaje = "El usuario ya existe";
        //        return new RegistroResponse { Exitoso = false, Mensaje = mensaje };
        //    }

        //    var usuario = new ApplicationUser
        //    {
        //        UserName = modelo.Email,
        //        Email = modelo.Email
        //    };
        //    var resultado = await userManager.CreateAsync(usuario, modelo.Password);

        //    if (!resultado.Succeeded)
        //    {
        //        mensaje= "No se pudo crear el usuario";
        //        var errores = string.Join(", ", resultado.Errors.Select(e => e.Description));
        //        return new RegistroResponse
        //        {
        //            Exitoso = false,
        //            Mensaje = $"No se pudo crear el usuario: {errores}"
        //        };
        //    }
        //    await context.SaveChangesAsync();
        //    var token = CreateToken(usuario);
        //    mensaje= "Usuario creado exitosamente";
        //    return new RegistroResponse { Exitoso = true, Mensaje = mensaje, Token=token };
        //}
        public async Task<RegistroResponse> Registro(RegistroUsuarioViewModel modelo)
        {
            try
            {
                var existeUsuario = await userManager.FindByEmailAsync(modelo.Email);
                if (existeUsuario != null)
                {
                    return new RegistroResponse { Exitoso = false, Mensaje = "El usuario ya existe" };
                }

                var usuario = new ApplicationUser { UserName = modelo.Email, Email = modelo.Email };
                var resultado = await userManager.CreateAsync(usuario, modelo.Password);

                if (!resultado.Succeeded)
                {
                    var errores = string.Join(", ", resultado.Errors.Select(e => e.Description));
                    return new RegistroResponse { Exitoso = false, Mensaje = $"No se pudo crear el usuario: {errores}" };
                }

                await context.SaveChangesAsync();

                var token = CreateToken(usuario);
                return new RegistroResponse { Exitoso = true, Mensaje = "Usuario creado exitosamente", Token = token };
            }
            catch (Exception ex)
            {
                // Atrapar cualquier excepción inesperada
                return new RegistroResponse { Exitoso = false, Mensaje = $"Error interno: {ex.Message}" };
            }
        }


        private string CreateToken(ApplicationUser user)
        {
            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Email, user.Email),
        new Claim(ClaimTypes.NameIdentifier, user.Id)
    };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(configuration["Jwt:Key"])
            );

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                issuer: configuration["Jwt:Issuer"],
                audience: configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(3),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
