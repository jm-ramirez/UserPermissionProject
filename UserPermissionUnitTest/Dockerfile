# Selecciona una imagen que contenga el entorno .NET Core SDK
FROM mcr.microsoft.com/dotnet/sdk:6.0

# Establece el directorio de trabajo
WORKDIR /app

# Copia los archivos del proyecto de pruebas
COPY . .

# Instala xUnit (asegúrate de que tu proyecto de pruebas tenga xUnit como dependencia)
RUN dotnet restore
RUN dotnet test
