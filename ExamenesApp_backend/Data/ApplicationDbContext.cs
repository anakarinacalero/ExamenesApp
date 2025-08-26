using ExamenesApp_Modelos;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace ExamenesApp_backend.Data
{
        public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
        {
            public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
                : base(options)
            {

            }
            public DbSet<Examen> Examens { get; set; }
            public DbSet<Inscripcion> Inscripciones { get; set; }
            public DbSet<Usuario> Usuario { get; set; }


            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                base.OnModelCreating(modelBuilder);
                // ==== Relación 1:1 ApplicationUser <-> Usuario (PK = FK en Usuario) ====
                modelBuilder.Entity<Usuario>()
                    .HasKey(u => u.Id);

                modelBuilder.Entity<ApplicationUser>()
                    .HasOne(a => a.Usuario)
                    .WithOne()
                    .HasForeignKey<Usuario>(u => u.Id) // Usuario.Id = ApplicationUser.Id
                    .OnDelete(DeleteBehavior.Cascade);
           
       
        
            // Indice unico para UsuarioId y ExamenId en Inscripcion
            modelBuilder.Entity<Inscripcion>()
                    .HasIndex(i => new { i.UsuarioId, i.ExamenId })
                    .IsUnique();

                //Codigo único
                modelBuilder.Entity<Inscripcion>()
                    .HasIndex(i => i.Codigo)
                    .IsUnique();

                // Relación Usuario - Inscripcion
                modelBuilder.Entity<Inscripcion>()
                    .HasOne(i => i.Usuario)
                    .WithMany(u => u.Inscripciones)
                    .HasForeignKey(i => i.UsuarioId)
                    .OnDelete(DeleteBehavior.Cascade);

                // Relación Examen - Inscripcion
                modelBuilder.Entity<Inscripcion>()
                    .HasOne(i => i.Examen)
                    .WithMany(e => e.Inscripciones)
                    .HasForeignKey(i => i.ExamenId)
                    .OnDelete(DeleteBehavior.Cascade);

                
            }
        }
    
}
