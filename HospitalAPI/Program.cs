using HospitalAPI.Data;
using HospitalAPI.Repositorios;
using HospitalAPI.Repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(x =>
   x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddEntityFrameworkSqlServer().AddDbContext<HospitalAPIContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DataBase")));

builder.Services.AddScoped<IMedicoRepositorios, MedicoRepositorios> ();
builder.Services.AddScoped<IPacienteRepositorios, PacienteRepositorios> ();
builder.Services.AddScoped<IConsultaRepositorios, ConsultaRepositorios> ();
builder.Services.AddScoped<IConvenioRepositorios, ConvenioRepositorios> ();
builder.Services.AddScoped<IRetornoRepositorios, RetornoRepositorios> ();



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
