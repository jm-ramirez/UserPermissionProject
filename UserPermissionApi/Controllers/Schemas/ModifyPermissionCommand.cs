namespace UserPermissionApi.Controllers.Schemas
{
    public class ModifyPermissionCommand
    {
        public int Id { get; set; }
        public string NombreEmpleado { get; set; } = string.Empty;
        public string ApellidoEmpleado { get; set; } = string.Empty;
        public string TipoPermisoNombre { get; set; } = string.Empty;
        public DateTime FechaPermiso { get; set; }
    }
}
