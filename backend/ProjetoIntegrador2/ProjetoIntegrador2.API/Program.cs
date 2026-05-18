using Microsoft.EntityFrameworkCore;
using ProjetoIntegrador2.API.Data;
using ProjetoIntegrador2.API.Data.Seeds;
using ProjetoIntegrador2.API.Mappings;

// Necessário para o Npgsql aceitar DateTime sem precisar ser UTC
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

var builder = WebApplication.CreateBuilder(args);

// Configura a conexão com o banco PostgreSQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// AutoMapper para converter entre entidades e DTOs
builder.Services.AddAutoMapper(typeof(ProdutoProfile));

// Permite que o frontend acesse a API (CORS liberado para desenvolvimento)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Cria as tabelas no banco se ainda não existirem e popula os dados iniciais
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
    MovimentoSeeder.Seed(db);
}

// Swagger só fica disponível em ambiente de desenvolvimento
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();

app.Run();
