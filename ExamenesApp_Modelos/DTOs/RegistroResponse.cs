using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamenesApp_Modelos.DTOs
{
    public class RegistroResponse
    {
        public bool Exitoso { get; set; }
        public string Token { get; set; } = null;
        public string Mensaje { get; set; }
    }
}
