namespace UserPermissionApi.Controllers.Schemas
{
    public class RequestPermissionCommand
    {
        public string NombreEmpleado { get; set; }
        public string ApellidoEmpleado { get; set; }
        public string TipoPermisoNombre { get; set; }
        public DateTime FechaPermiso { get; set; }

        public RequestPermissionCommand(string nombreEmpleado, string apellidoEmpleado, string tipoPermisoNombre, DateTime fechaPermiso)
        {
            NombreEmpleado = nombreEmpleado;
            ApellidoEmpleado = apellidoEmpleado;
            TipoPermisoNombre = tipoPermisoNombre;
            FechaPermiso = fechaPermiso;
        }
    }
}
