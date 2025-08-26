using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamenesApp_Modelos
{
    public class Inscripcion
    {
        public int Id { get; set; }

        public string Codigo { get; set; }
        public DateTime FechaExamen { get; set; }

        public DateTime FechaLimiteCancelacion { get; set; }
        // Relaciones
        public string UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

        public int ExamenId { get; set; }
        public Examen Examen { get; set; }
    }
}
