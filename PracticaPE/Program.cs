using Microsoft.EntityFrameworkCore;
using PracticaPE.Data;

var builder = WebApplication.CreateBuilder(args);

// 1) Connection string (lee de appsettings.json o de Azure → Configuration → Connection strings)
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                       ?? throw new InvalidOperationException("Falta la cadena 'DefaultConnection'.");

// 2) EF Core con resiliencia para Azure SQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString, sql =>
        sql.EnableRetryOnFailure(maxRetryCount: 5, maxRetryDelay: TimeSpan.FromSeconds(10), errorNumbersToAdd: null)));

builder.Services.AddControllers();

// 3) Swagger / OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// 4) Swagger SIEMPRE (Prod también) y raíz redirige a /swagger
app.UseSwagger();
app.UseSwaggerUI();
app.MapGet("/", () => Results.Redirect("/swagger"));

// 5) Aplicar migraciones al iniciar (crea/actualiza tablas en Azure)
using (var scope = app.Services.CreateScope())
{
    try
    {
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        db.Database.Migrate();
        app.Logger.LogInformation("Migraciones aplicadas correctamente.");
    }
    catch (Exception ex)
    {
        app.Logger.LogError(ex, "Error aplicando migraciones.");
        // Si prefieres fallar duro en caso de error de DB:
        // throw;
    }
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();