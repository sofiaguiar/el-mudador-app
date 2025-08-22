using ElMudador.Api.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// CORS (dev)
var corsPolicy = "_dev";
builder.Services.AddCors(o =>
    o.AddPolicy(corsPolicy, p => p.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

// PostgreSQL
builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseNpgsql(builder.Configuration.GetConnectionString("Postgres")));

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors(corsPolicy);
app.UseSwagger();
app.UseSwaggerUI();

// Health
app.MapGet("/health", () => "ok");

// Endpoint EF de prueba
app.MapGet("/clientes", async (AppDbContext db) => await db.Clientes.ToListAsync());

// Seed inicial
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
    if (!db.Clientes.Any())
    {
        db.Clientes.Add(new ElMudador.Api.Domain.Entities.Cliente { RazonSocial = "Cliente Demo" });
        db.SaveChanges();
    }
}

app.Run();

