using Microsoft.EntityFrameworkCore;
using UserPermissionApi.Data;
using UserPermissionApi.Repositories;
using UserPermissionApi.Services;
using UserPermissionApi.ElasticSearch;
using UserPermissionApi.Model;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<UserPermissionDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.")));

builder.Services.AddScoped<IPermissionRepository<Permissions>, PermissionRepository>();
builder.Services.AddScoped<IPermissionTypeRepository<PermissionTypes>, PermissionTypeRepository>();

builder.Services.AddScoped<IRequestPermissionService, RequestPermissionService>();
builder.Services.AddScoped<IModifyPermissionService, ModifyPermissionService>();
builder.Services.AddScoped<IGetPermissionService, GetPermissionService>();
builder.Services.AddScoped<IGetPermissionTypesService, GetPermissionTypesService>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>(); 

builder.Services.Configure<ElasticsearchSettings>(builder.Configuration.GetSection("ElasticsearchSettings"));
builder.Services.AddSingleton<ElasticsearchClientProvider>();

builder.Services.Configure<KafkaConfiguration>(builder.Configuration.GetSection("KafkaConfiguration"));
builder.Services.AddScoped<IKafkaProducer, KafkaProducer>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder
            .AllowAnyOrigin() // Permitir solicitudes desde cualquier origen
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors();

app.MapControllers();

app.Run();
