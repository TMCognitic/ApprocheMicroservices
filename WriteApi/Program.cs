using System.Data.Common;
using BStorm.Tools.CommandQuerySeparation.Commands;
using Microsoft.Data.SqlClient;
using WriteApi.Models.Commands;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<DbConnection>(sp => new SqlConnection(config.GetConnectionString("Default")!));
builder.Services.AddScoped<ICommandHandler<CreateMessageCommand>, CreateMessageCommandHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseAuthorization();

app.MapControllers();

app.Run();
