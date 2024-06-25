using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Garbage.Collection.API.ViewModels;
using Garbage.Collection.Data.Context;
using Garbage.Collection.Data.Repository.Interfaces;
using Garbage.Collection.Data.Repository;
using Garbage.Collection.Business.Service;
using Garbage.Collection.Business.Service.Interfaces;
using Garbage.Collection.Data.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Add DbContext configuration
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ICaminhaoRepository, CaminhaoRepository>();
builder.Services.AddScoped<ICaminhaoService, CaminhaoService>();

builder.Services.AddScoped<IAgendamentoRepository, AgendamentoRepository>();
builder.Services.AddScoped<IAgendamentoService, AgendamentoService>();

builder.Services.AddScoped<IEnderecoRepository, EnderecoRepository>();
builder.Services.AddScoped<IEnderecoService, EnderecoService>();


var mapperConfig = new AutoMapper.MapperConfiguration(c => {
    // Permite que coleções nulas sejam mapeadas
    c.AllowNullCollections = true;
    // Permite que valores de destino nulos sejam mapeados
    c.AllowNullDestinationValues = true;

    c.CreateMap<Caminhao, CaminhaoViewModel>().ReverseMap();
    c.CreateMap<Caminhao, CaminhaoUpdateViewModel>().ReverseMap();

    c.CreateMap<Endereco, EnderecoViewModel>().ReverseMap();

});

// Cria o mapper com base na configuração definida
IMapper mapper = mapperConfig.CreateMapper();

// Registra o IMapper como um serviço singleton no container de DI do ASP.NET Core
builder.Services.AddSingleton(mapper);


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
