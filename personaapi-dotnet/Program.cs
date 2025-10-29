using Microsoft.EntityFrameworkCore;
using personaapi_dotnet.Models.Entities;
using personaapi_dotnet.Repositories;

var builder = WebApplication.CreateBuilder(args);


// Agregar la cadena de conexi�n (EF Core)

builder.Services.AddDbContext<PersonaDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConexionSQL")));

// Configuraci�n b�sica de MVC y Swagger

builder.Services.AddControllersWithViews();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

// Configurar pol�tica de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});

var app = builder.Build();

// Middleware del pipeline HTTP

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseCors("AllowAll");
app.UseAuthorization();

app.MapControllers();
app.MapDefaultControllerRoute();

app.Run();
