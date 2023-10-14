using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserPermissionApi.Model
{
    public class Permissions
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Description("Unique ID")]
        public int Id { get; set; }

        [Required]
        [Description("Employee Forename")]
        public string NombreEmpleado { get; set; } = string.Empty;

        [Required]
        [Description("Employee Surname")]
        public string ApellidoEmpleado { get; set; } = string.Empty;

        [Required]
        [Description("Permission granted on Date")]
        public DateTime FechaPermiso { get; set; }

        [Required]
        [Description("Permission Type")]
        public int TipoPermiso { get; set; } // Clave foránea
        public PermissionTypes? PermissionTypes { get; set; } // Propiedad de navegación
    }
}
