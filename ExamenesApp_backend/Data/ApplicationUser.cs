using ExamenesApp_Modelos;
using Microsoft.AspNetCore.Identity;

namespace ExamenesApp_backend.Data
{
    public class ApplicationUser:IdentityUser
    {
        // Propiedad de navegación 1:1
        public Usuario Usuario { get; set; }
    }
}
