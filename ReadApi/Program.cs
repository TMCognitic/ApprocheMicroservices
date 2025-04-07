using System.Data.Common;
using BStorm.Tools.CommandQuerySeparation.Commands;
using BStorm.Tools.CommandQuerySeparation.Queries;
using Microsoft.Data.SqlClient;
using ReadApi.Models.Commands;
using ReadApi.Models.Entities;
using ReadApi.Models.Queries;
using ReadApi.Workers;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddHostedService<ReadFileWorker>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<DbConnection>(sp => new SqlConnection(config.GetConnectionString("Default")));
builder.Services.AddScoped<ICommandHandler<AddMessageCommand>, AddMessageCommandHandler>();
builder.Services.AddScoped<IQueryHandler<GetMessagesQuery, IEnumerable<Message>>, GetMessagesQueryHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseAuthorization();
app.UseCors(o => { o.AllowAnyHeader(); o.AllowAnyMethod(); o.AllowAnyOrigin(); });

app.MapControllers();

app.Run();
