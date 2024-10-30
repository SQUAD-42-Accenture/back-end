using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SERVPRO.Data;
using SERVPRO.Repositorios;
using SERVPRO.Repositorios.interfaces;
using System.Text;
using System.Text.Json.Serialization;
using FluentValidation;
using FluentValidation.AspNetCore;

string chaveSecreta = "3c728fbf-7290-4087-b180-7fead6e5bbe6";
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
    });
builder.Services.AddFluentValidationAutoValidation();
//builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<Program>();


builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ClientePolicy", policy =>
        policy.RequireAssertion(context =>
            context.User.HasClaim("tipoUsuario", "Cliente") ||
            context.User.HasClaim("tipoUsuario", "Administrador")));

    options.AddPolicy("TecnicoPolicy", policy =>
        policy.RequireAssertion(context =>
            context.User.HasClaim("tipoUsuario", "Tecnico") ||
            context.User.HasClaim("tipoUsuario", "Administrador")));

    options.AddPolicy("AdministradorPolicy", policy =>
       policy.RequireClaim("tipoUsuario", "Administrador"));
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy => policy.WithOrigins("http://localhost:5173") // URL do front-end React
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Servpro - API", Version = "v1" });
    var securitySchema = new OpenApiSecurityScheme
    {
        Name = "JWT Autenticação",
        Description = "Entre com o JWT Bear token",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };
    c.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, securitySchema);
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { securitySchema, new string[] { } }
    });
});

builder.Services.AddDbContext<ServproDBContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("conexaopadrao"))
);

builder.Services.AddScoped<IClienteRepositorio, ClienteRepositorio>();
builder.Services.AddScoped< IAdministradorRepositorio, AdministradorRepositorio>();
builder.Services.AddScoped<ITecnicoRepositorio, TecnicoRepositorio>();
builder.Services.AddScoped<IEquipamentoRepositorio, EquipamentoRepositorio>();
builder.Services.AddScoped<IOrdemDeServicoRepositorio, OrdemdeServicoRepositorio>();
builder.Services.AddScoped<IHistoricoOsRepositorio, HistoricoOsRepositorio>();
builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
builder.Services.AddScoped<PdfServiceRepositorio>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "sua_empresa",
        ValidAudience = "sua_aplicacao",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(chaveSecreta))
    };
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Use(async (context, next) =>
{
    await next();

    if (context.Response.StatusCode == StatusCodes.Status403Forbidden)
    {
        context.Response.ContentType = "application/json";

        // Identificar o tipo de usuário a partir dos claims
        var userClaims = context.User.Claims;
        var tipoUsuario = userClaims.FirstOrDefault(c => c.Type == "tipoUsuario")?.Value;

        // Mensagens personalizadas para cada tipo de usuário
        var mensagem = tipoUsuario switch
        {
            "Tecnico" => "Acesso negado. Apenas para técnicos.",
            "Cliente" => "Acesso negado. Apenas para clientes.",
            //"Administrador" => "Acesso negado. Esta área é restrita a administradores.",
            _ => "Acesso negado. Permissão insuficiente."
        };

        await context.Response.WriteAsync($"{{\"mensagem\": \"{mensagem}\"}}");
    }
    else if (context.Response.StatusCode == StatusCodes.Status401Unauthorized)
    {
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsync("{\"mensagem\": \"Não autorizado. Por favor, faça login para acessar esta área.\"}");
    }
});

app.UseHttpsRedirection();
app.UseCors("AllowReactApp");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
