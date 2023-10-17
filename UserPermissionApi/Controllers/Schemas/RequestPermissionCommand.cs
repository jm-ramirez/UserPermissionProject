namespace UserPermissionApi.Controllers.Schemas
{
    public class RequestPermissionCommand
    {
        public string NombreEmpleado { get; set; } = string.Empty;
        public string ApellidoEmpleado { get; set; } = string.Empty;
        public string TipoPermisoNombre { get; set; } = string.Empty;
        public DateTime FechaPermiso { get; set; }

        //public RequestPermissionCommand(string nombreEmpleado, string apellidoEmpleado, string tipoPermisoNombre, DateTime fechaPermiso)
        //{
        //    NombreEmpleado = nombreEmpleado;
        //    ApellidoEmpleado = apellidoEmpleado;
        //    TipoPermisoNombre = tipoPermisoNombre;
        //    FechaPermiso = fechaPermiso;
        //}
    }
}
