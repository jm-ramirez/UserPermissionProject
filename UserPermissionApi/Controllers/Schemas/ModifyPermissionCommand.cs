namespace UserPermissionApi.Controllers.Schemas
{
    public class ModifyPermissionCommand
    {
        public int Id { get; set; }
        public string NombreEmpleado { get; set; }
        public string ApellidoEmpleado { get; set; }
        public string TipoPermisoNombre { get; set; }
        public DateTime FechaPermiso { get; set; }

        public ModifyPermissionCommand(int id, string nombreEmpleado, string apellidoEmpleado, string tipoPermisoNombre, DateTime fechaPermiso)
        {
            Id = id;
            NombreEmpleado = nombreEmpleado;
            ApellidoEmpleado = apellidoEmpleado;
            TipoPermisoNombre = tipoPermisoNombre;
            FechaPermiso = fechaPermiso;
        }
    }
}
