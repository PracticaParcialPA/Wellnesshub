using Microsoft.EntityFrameworkCore;
using PracticaPE.Data; // Asegúrate que el namespace coincida con tu AppDbContext

var builder = WebApplication.CreateBuilder(args);

// 🔹 Configurar la cadena de conexión de Azure SQL
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// 🔹 Registrar DbContext con SQL Server
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

// 🔹 Registrar servicios de controladores (API)
builder.Services.AddControllers();

// 🔹 Configurar Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// 🔹 Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

// 🔹 Mapear controladores
app.MapControllers();

app.Run();