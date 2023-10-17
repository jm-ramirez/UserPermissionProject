# Selecciona la imagen base de ASP.NET Core
FROM mcr.microsoft.com/dotnet/aspnet:6.0

# Establece el directorio de trabajo
WORKDIR /app

# Copia la aplicación publicada en el contenedor
COPY ./bin/Release/net6.0 .

# Define el comando de inicio
ENTRYPOINT ["dotnet", "UserPermissionApi.dll"]