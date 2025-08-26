using AutoMapper;
using ExamenesApp_backend.Data;
using ExamenesApp_backend.Servicios.IServicios;
using ExamenesApp_Modelos;
using ExamenesApp_Modelos.DTOs;
using Microsoft.EntityFrameworkCore;

namespace ExamenesApp_backend.Servicios
{
    public class ServicioInscripcionExamen : IServicioInscripcionExamen
    {
        private readonly IServicioPerfilUsuario servicioPerfilUsuario;
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public ServicioInscripcionExamen(IServicioPerfilUsuario servicioPerfilUsuario,
            ApplicationDbContext context, IMapper mapper)
        {
            this.servicioPerfilUsuario = servicioPerfilUsuario;
            this.context = context;
            this.mapper = mapper;
        }
        public async Task<bool> EliminarInscripcionExamen(int id)
        {
            var userId = servicioPerfilUsuario.ObtenerIdUsuario();
            if (userId == null)
            {
                throw new UnauthorizedAccessException("No se encontró el usuario autenticado.");
            }
            var inscripcion = await context.Inscripciones.Where(x => x.UsuarioId == userId && x.Id == id)
                .FirstOrDefaultAsync();
            if (inscripcion is null)
            {
                return false;
            }
            context.Inscripciones.Remove(inscripcion);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<InscripcionExamenDTO>> ObtenerInscripcionesUsuarioAsync()
        {
            var userId = servicioPerfilUsuario.ObtenerIdUsuario();
            if (userId == null)
            {
                throw new UnauthorizedAccessException("No se encontró el usuario autenticado.");
            }

            var incripcionesUsuario = await context.Inscripciones.Where(u => u.UsuarioId == userId).Include(i => i.Examen).Include(i => i.Usuario)
               .Select(i => new InscripcionExamenDTO
               {
                   Id = i.Id,
                   Codigo = i.Codigo,
                   FechaExamen = i.FechaExamen,
                   UsuarioId = i.UsuarioId,
                   ExamenId = i.ExamenId,
                  NombreExamen= i.Examen.Nombre,
                   
               })
       .ToListAsync();

            return incripcionesUsuario;
        }
        

        public async Task<InscripcionExamenDTO> RegistarInscripcionExamenes(int examenId)
        {
            var userId = servicioPerfilUsuario.ObtenerIdUsuario();
            if (userId == null)
            {
                throw new UnauthorizedAccessException("No se encontró el usuario autenticado.");
            }
            // Validar que no se repita el examen
            var existe = await context.Inscripciones.AnyAsync(i => i.UsuarioId == userId && i.Examen.Id == examenId);
            if (existe)
            {
                return null;
            }


            var random = GenerarCodigo();


            var examen = await context.Examens.FirstOrDefaultAsync(e => e.Id == examenId);
            var fechaExamen = await GenerarFechaAleatoria();
            var inscripcion = new InscripcionExamenDTO
            {

                UsuarioId = userId,
                ExamenId = examen.Id,
                NombreExamen= examen.Nombre,
                FechaExamen = fechaExamen,
                FechaLimiteCancelacion = fechaExamen.AddDays(-1),
                Codigo = random,
               
            };

            var nuevaInscripcion = mapper.Map<Inscripcion>(inscripcion);
            context.Inscripciones.Add(nuevaInscripcion);
            await context.SaveChangesAsync();
            return mapper.Map<InscripcionExamenDTO>(nuevaInscripcion);
        }
        private string GenerarCodigo()
        {
            // codigo de 8 caracteres
            var random = Guid.NewGuid().ToString().Substring(0, 8).ToUpper();
            return random;
        }
        private async Task<DateTime> GenerarFechaAleatoria()
        {
            var userId =  servicioPerfilUsuario.ObtenerIdUsuario();
            if (userId == null)
            {
                throw new UnauthorizedAccessException("No se encontró el usuario autenticado.");
            }
            var horario = await context.Usuario.Where(x => x.Id == userId).Select(x => x.HorarioPreferencia).FirstOrDefaultAsync();
            Random random = new Random();
            DateTime fecha = new DateTime();

            fecha = DateTime.Today.AddDays(random.Next(1, 31));
            // Si no existe, usa un valor por defecto
            if (string.IsNullOrEmpty(horario))
            {
                horario = "Tarde";
            }

            switch (horario)
            {
                case "Mañana":
                    fecha = fecha.AddHours(random.Next(8, 13));
                    break;
                case "Tarde":
                    fecha = fecha.AddHours(random.Next(13, 18));
                    break;
                case "Noche":
                    fecha = fecha.AddHours(random.Next(18, 22));
                    break;
                default:

                    break;
            }
            return fecha;
        }
        
       
    }
}
