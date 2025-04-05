using FluentValidation.AspNetCore;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SERVPRO.Data;
using SERVPRO.Repositorios;
using SERVPRO.Repositorios.interfaces;
using SERVPRO.Models;
using Microsoft.Extensions.FileProviders;
using SERVPRO.Repositorios.Interfaces;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Controllers e JSON
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
    });

// FluentValidation
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

// Autorização
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

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
        policy.WithOrigins(
                "http://localhost:5173",
                "https://servpro.vercel.app",
                "https://front-end-c3nt.onrender.com")
              .AllowAnyMethod()
              .AllowAnyHeader());
});

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Servpro - API", Version = "v1" });
    var securitySchema = new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "Entre com o token JWT",
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
        { securitySchema, new[] { JwtBearerDefaults.AuthenticationScheme } }
    });
});

// Banco de Dados
builder.Services.AddDbContext<ServproDBContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// Repositórios
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

// Autenticação JWT
var chaveSecreta = "3c728fbf-7290-4087-b180-7fead6e5bbe6";
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

// Atualiza banco & insere dados iniciais
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ServproDBContext>();
    context.Database.Migrate();

    if (!context.Usuarios.Any())
    {
        context.Usuarios.AddRange(
            new Usuario { Nome = "SERVPRO_ADM", Email = "adm_servPro@gmail.com", CPF = "12312312312", Senha = "admin", TipoUsuario = "Administrador" },
            new Usuario { Nome = "Ana Clara Rodrigues", Email = "anaB@gmail.com", CPF = "65908304280", Senha = "123adm", TipoUsuario = "Cliente" },
            new Usuario { Nome = "Paulo Pinho", Email = "PauloP@gmail.com", CPF = "71134258447", Senha = "1234", TipoUsuario = "Tecnico" }
        );
        context.SaveChanges();
    }
}

// Middleware padrão
app.UseHttpsRedirection();
app.UseCors("AllowReactApp");
app.UseAuthentication();
app.UseAuthorization();

// Swagger UI
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Servpro API V1");

});

app.Use(async (context, next) =>
{
    await next();

    if (context.Response.StatusCode == StatusCodes.Status403Forbidden)
    {
        context.Response.ContentType = "application/json";

        var tipoUsuario = context.User?.Claims
            .FirstOrDefault(c => c.Type == "tipoUsuario")?.Value;

        var mensagem = tipoUsuario switch
        {
            "Tecnico" => "Acesso negado. Técnicos não podem acessar.",
            "Cliente" => "Acesso negado. Clientes não podem acessar.",
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

// Arquivos estáticos
var pastaFotos = Path.Combine(Directory.GetCurrentDirectory(), "FotosClientes");
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(pastaFotos),
    RequestPath = "/Fotos"
});

// Map Controllers
app.MapControllers();

app.Run();
