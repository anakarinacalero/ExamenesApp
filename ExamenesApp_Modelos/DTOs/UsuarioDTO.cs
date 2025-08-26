

using System.ComponentModel.DataAnnotations;

namespace ExamenesApp_Modelos.DTOs
{
    public class UsuarioDTO
    {

        [Required(ErrorMessage = "Los nombres son obligatorios.")]
        [StringLength(25, ErrorMessage = "Los nombres no pueden tener más de 25 caracteres.")]
        public string Nombres { get; set; }

        [Required(ErrorMessage = "Los apellidos son obligatorios.")]
        [StringLength(25, ErrorMessage = "Los apellidos no pueden tener más de 25 caracteres.")]
        public string Apellidos { get; set; }

        [Required(ErrorMessage = "La fecha de nacimiento es obligatoria.")]
        [DataType(DataType.Date)]
        [CustomValidation(typeof(UsuarioDTO), nameof(ValidarFechaNacimiento))]
        public DateTime FechaDeNacimiento { get; set; } = new DateTime(2005, 01, 01);

        [Required(ErrorMessage = "La dirección es obligatoria.")]
        [StringLength(200)]
        public string Direccion { get; set; }

        [Required(ErrorMessage = "El género es obligatorio.")]
        public string Genero { get; set; }

        [Required(ErrorMessage = "El CURP es obligatorio.")]
        [RegularExpression(@"^[A-Z]{4}\d{6}[HM][A-Z]{5}[A-Z0-9]\d$",
        ErrorMessage = "CURP inválido.")]
        public string CURP { get; set; } = "GARC800101HDFABC09";

        [Required(ErrorMessage = "El teléfono es obligatorio.")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "El teléfono debe tener exactamente 10 números.")]
       
        public string Telefono { get; set; }

        [Required(ErrorMessage = "La modalidad es obligatoria.")]
        public string Modalidad { get; set; }
        public bool Asistencia { get; set; }

        public string? DescripcionPersonal { get; set; }

        public string? HorarioPreferencia { get; set; }

        public bool VariosExamenes { get; set; }
        public List<string> ExamenesSeleccionados { get; set; } = new List<string>() { "" };


        public static ValidationResult? ValidarFechaNacimiento(DateTime fechaNacimiento, ValidationContext context)
        {
            if (fechaNacimiento > DateTime.Now)
            {
                return new ValidationResult("La fecha de nacimiento no puede ser en el futuro.");
            }
            var edad = DateTime.Today.Year - fechaNacimiento.Year;
            if (fechaNacimiento.Date > DateTime.Today.AddYears(-edad)) edad--;
            if (edad > 100)
            {
                return new ValidationResult("Ingrese una fecha válida");
            }
            if (edad < 16)
            {
                return new ValidationResult("La edad miníma para registrarse es 16");
            }
            return ValidationResult.Success;

        }
    }
}

