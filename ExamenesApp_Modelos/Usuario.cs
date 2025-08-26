using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ExamenesApp_Modelos;

public class Usuario
{

    [Key]                       // PK
  
    public string Id { get; set; }
    public string Nombres { get; set; }
    public string Apellidos { get; set; }
    public DateTime FechaDeNacimiento { get; set; }
    public string Direccion { get; set; }
    public string Genero { get; set; }
    public string CURP { get; set; }
    public string Telefono { get; set; }
    public string Modalidad { get; set; }
    public bool Asistencia { get; set; }
    public string? DescripcionPersonal { get; set; }
    public string? HorarioPreferencia { get; set; }
    public bool? VariosExamenes { get; set; }
    public string? Cuales { get; set; }
    // Navegación inversa (1:1)
   
    public ICollection<Inscripcion> Inscripciones { get; set; } = new List<Inscripcion>();
}
