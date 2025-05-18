using patioAPI.Models;
using patioAPI.Services;
using patioAPI;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Configuração do contexto EF Core com Oracle
builder.Services.AddDbContext<PatioContext>(options =>
    options.UseOracle(builder.Configuration.GetConnectionString("OracleConnection")));

// Registra MotoService
builder.Services.AddScoped<MotoService>();
// Registra PatioService
builder.Services.AddScoped<PatioService>();
// Registra VeiculoPatioService
builder.Services.AddScoped<VeiculoPatioService>();
// registra filialService
builder.Services.AddScoped<FilialService>(); // <-- Adicione esta linha
// Adiciona controllers
builder.Services.AddControllers();

// Configuração do CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

// Configuração do Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Patio API", Version = "v1" });
    // Adiciona exemplos de resposta e erro para todas as rotas
    c.OperationFilter<SwaggerDefaultResponseExamplesFilter>();
});

var app = builder.Build();

// Middleware do Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();

