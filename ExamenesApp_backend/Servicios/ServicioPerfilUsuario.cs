using AutoMapper;
using ExamenesApp_backend.Data;
using ExamenesApp_backend.Servicios.IServicios;
using ExamenesApp_Modelos;
using ExamenesApp_Modelos.DTOs;
using ExamenesApp_Modelos.ViewModels;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ExamenesApp_backend.Servicios
{
    public class ServicioPerfilUsuario : IServicioPerfilUsuario
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<ApplicationUser> userManager;
       
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IMapper mapper;

        public ServicioPerfilUsuario(ApplicationDbContext context, IMapper mapper,
            UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor
            )
        {
            this.context = context;
            this.mapper = mapper;
            this.userManager = userManager;
            
            this.httpContextAccessor = httpContextAccessor;
        }
        public async Task<bool> DatosExisten()
        {
            var userId =  ObtenerIdUsuario();
            if (userId == null)
            {
                throw new UnauthorizedAccessException("No se encontró el usuario autenticado.");
            }
            var existe = await context.Usuario.Where(u => u.Id == userId && !string.IsNullOrEmpty(u.Nombres)).AnyAsync();
            return existe;
        }

        public async Task<UsuarioDTO> GetPerfilPorUserIdAsync(string userId)
        {
            var usuario = await context.Usuario
              .AsNoTracking() // solo lectura
              .FirstOrDefaultAsync(u => u.Id == userId);

            if (usuario != null)
            {
                return mapper.Map<UsuarioDTO>(usuario);
            }
            return null;
        }

        public async Task<UsuarioDTO> GuardarDatosAsync(UsuarioDTO usuario)
        {
            var userId = ObtenerIdUsuario();
            if (userId == null)
            {

                throw new UnauthorizedAccessException("No se encontró el usuario autenticado.");
            }

            var perfil = await context.Usuario.FirstOrDefaultAsync(u=>u.Id == userId );

            if (perfil == null)
            {
                var nuevoUsuario = mapper.Map<Usuario>(usuario);
                nuevoUsuario.Id = userId;
                context.Usuario.Add(nuevoUsuario);
                await context.SaveChangesAsync();

                return mapper.Map<UsuarioDTO>(nuevoUsuario);
            }
            else
            {
                mapper.Map(usuario, perfil);
                context.Usuario.Update(perfil);
                await context.SaveChangesAsync();
                return mapper.Map<UsuarioDTO>(perfil);
            }
        }

        // Obtener el ID del usuario autenticado 
        public string ObtenerIdUsuario()
        {
            var usuario = httpContextAccessor.HttpContext?.User;
            if (usuario == null)
            {
                return null;
            }
            return userManager.GetUserId(usuario);
        }
    }
}
