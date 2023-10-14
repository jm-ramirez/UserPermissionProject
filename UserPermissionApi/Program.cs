using Microsoft.EntityFrameworkCore;
using UserPermissionApi.Data;
using UserPermissionApi.Repositories;
using UserPermissionApi.Services;
using UserPermissionApi.ElasticSearch;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<UserPermissionDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.")));

builder.Services.AddScoped<IPermissionRepository, PermissionRepository>();
builder.Services.AddScoped<IPermissionTypeRepository, PermissionTypeRepository>();

builder.Services.AddScoped<IRequestPermissionService, RequestPermissionService>();
builder.Services.AddScoped<IModifyPermissionService, ModifyPermissionService>();
builder.Services.AddScoped<IGetPermissionService, GetPermissionService>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>(); 

builder.Services.Configure<ElasticsearchSettings>(builder.Configuration.GetSection("ElasticsearchSettings"));
builder.Services.AddSingleton<ElasticsearchClientProvider>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
