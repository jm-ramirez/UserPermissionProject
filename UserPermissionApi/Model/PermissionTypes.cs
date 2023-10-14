using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserPermissionApi.Model
{
    public class PermissionTypes
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Description("Unique ID")]
        public int Id { get; set; }

        [Required]
        [Description("Permission description")]
        public string Descripcion { get; set; } = string.Empty;

        public ICollection<Permissions>? Permissions { get; set; } // Propiedad de navegación inversa
    }
}
