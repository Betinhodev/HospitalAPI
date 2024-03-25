
using HospitalAPI.Data;
using HospitalAPI.Repositorios;
using HospitalAPI.Repositorios.Interfaces;
using HospitalAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(x =>
   x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer" 
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,
            },
            new List<string>()
            {

            }
        }
    });
});

var key = Encoding.ASCII.GetBytes(HospitalAPI.Services.AuthenticationService.Key.Secret);

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

builder.Services.AddEntityFrameworkSqlServer().AddDbContext<HospitalAPIContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DataBase")));
//builder.Services.AddDbContext<HospitalAPIContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("SQLite")));

builder.Services.AddScoped<IMedicoRepositorios, MedicoRepositorios> ();
builder.Services.AddScoped<IPacienteRepositorios, PacienteRepositorios> ();
builder.Services.AddScoped<IConsultaRepositorios, ConsultaRepositorios> ();
builder.Services.AddScoped<IConvenioRepositorios, ConvenioRepositorios> ();
builder.Services.AddScoped<IRetornoRepositorios, RetornoRepositorios> ();
builder.Services.AddScoped<AuthenticationService>();


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
