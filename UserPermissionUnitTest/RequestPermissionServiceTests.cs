using Xunit;
using UserPermissionApi.Model;
using UserPermissionApi.Repositories;
using UserPermissionApi.Services;
using Moq;
using Nest;
using UserPermissionApi.Controllers.Schemas;
using System.Threading.Tasks;

namespace UserPermissionUnitTest
{
    public class RequestPermissionServiceTests
    {
        [Fact]
        public async Task RequestPermissionService_ProcessPermissionRequest_Should_Create_PermissionWithPermissionType()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockPermissionTypesRepository = new Mock<IPermissionTypeRepository<PermissionTypes>>();
            var mockPermissionsRepository = new Mock<IPermissionRepository<Permissions>>();

            // Simulo el comportamiento de los métodos GetByNameAsync, AddAsync y SaveChangesAsync en IUnitOfWork
            mockUnitOfWork.Setup(uow => uow.PermissionTypes).Returns(mockPermissionTypesRepository.Object);
            mockUnitOfWork.Setup(uow => uow.Permissions).Returns(mockPermissionsRepository.Object);

            var requestPermissionService = new RequestPermissionService(mockUnitOfWork.Object);
            var requestCommand = new RequestPermissionCommand
            {
                NombreEmpleado = "Juan",
                ApellidoEmpleado = "Ramirez",
                FechaPermiso = System.DateTime.Now,
                TipoPermisoNombre = "Desarrollador", 
            };

            mockPermissionTypesRepository.Setup(repo => repo.GetByNameAsync(It.IsAny<string>()))
                .ReturnsAsync((string typeName) =>
                {
                    // Simulo el comportamiento de GetByNameAsync
                    if (typeName == requestCommand.TipoPermisoNombre)
                    {
                        return null; // El tipo de permiso no existe
                    }
                    return new PermissionTypes { Id = 1, Descripcion = typeName };
                });

            var result = await requestPermissionService.Request(requestCommand);

            Assert.True(result == Result.Created);
            // Verifico que se haya creado el permiso con el tipo de permiso correcto en mockPermissionsRepository
            mockPermissionsRepository.Verify(repo => repo.AddAsync(It.IsAny<Permissions>()), Times.Once);

            return;
        }
    }
}