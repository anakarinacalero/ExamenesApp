using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamenesApp_Modelos.DTOs
{
    public class InscripcionExamenDTO
    {

        public int Id { get; set; }
        
        public DateTime FechaExamen { get; set; }
        public DateTime FechaLimiteCancelacion { get; set; }
      
        public string Codigo { get; set; }

        public string UsuarioId { get; set; }

        public int ExamenId { get; set; }

        public string NombreExamen { get; set; }
       


    }
}
