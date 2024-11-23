using FluentValidation.AspNetCore;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SERVPRO.Data;
using SERVPRO.Repositorios;
using SERVPRO.Repositorios.interfaces;
using System.Text;
using System.Text.Json.Serialization;
using SERVPRO.Models;
using Microsoft.Extensions.FileProviders;
using SERVPRO.Repositorios.Interfaces;

string chaveSecreta = "3c728fbf-7290-4087-b180-7fead6e5bbe6";
var builder = WebApplication.CreateBuilder(args);

// Configuração do controller e JsonOptions
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
    });

// Configuração de validação fluente
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

// Configuração de autorização
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

// Configuração do CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy => policy.WithOrigins("http://localhost:5173", "https://servpro.vercel.app", "https://front-end-c3nt.onrender.com") // URL do front-end React
                        .AllowAnyMethod()
                        .AllowAnyHeader());
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

// Configuração dos repositórios
builder.Services.AddScoped<IClienteRepositorio, ClienteRepositorio>();
builder.Services.AddScoped<IAdministradorRepositorio, AdministradorRepositorio>();
builder.Services.AddScoped<ITecnicoRepositorio, TecnicoRepositorio>();
builder.Services.AddScoped<IEquipamentoRepositorio, EquipamentoRepositorio>();
builder.Services.AddScoped<IOrdemDeServicoRepositorio, OrdemDeServicoRepositorio>();
builder.Services.AddScoped<IHistoricoOsRepositorio, HistoricoOsRepositorio>();
builder.Services.AddScoped<IProdutoRepositorio, ProdutoRepositorio>();
builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
builder.Services.AddScoped<IServicoRepositorio, ServicoRepositorio>();
builder.Services.AddScoped<IServicoProdutoRepositorio, ServicoProdutoRepositorio>();
builder.Services.AddScoped<PdfServiceRepositorio>();
builder.Services.AddScoped<EmailServiceRepositorio>();

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

// Configuração do Swagger para todos os ambientes
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Servpro API V1");
    c.RoutePrefix = string.Empty; // Coloca o Swagger na raiz (opcional)
});


app.Use(async (context, next) =>
{
    await next();

    if (context.Response.StatusCode == StatusCodes.Status403Forbidden)
    {
        context.Response.ContentType = "application/json";

        if (context.User?.Claims != null) 
        {
            var userClaims = context.User.Claims;
            var tipoUsuario = userClaims.FirstOrDefault(c => c.Type == "tipoUsuario")?.Value;

            var mensagem = tipoUsuario switch
            {
                "Tecnico" => "Acesso negado. Técnicos não podem acessar.",
                "Cliente" => "Acesso negado. Clientes não podem acessar.",
                _ => "Acesso negado. Permissão insuficiente."
            };

            await context.Response.WriteAsync($"{{\"mensagem\": \"{mensagem}\"}}");
        }
        else
        {
            await context.Response.WriteAsync("{\"mensagem\": \"Acesso negado. Usuário não autenticado.\"}");
        }
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

var pastaFotos = Path.Combine(Directory.GetCurrentDirectory(), "FotosClientes");
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(pastaFotos),
    RequestPath = "/Fotos" // O caminho público para acessar as fotos será /Fotos/{nome_do_arquivo}
});

app.MapControllers();

var port = Environment.GetEnvironmentVariable("PORT") ?? "5000"; 
app.Run($"http://0.0.0.0:{port}");
