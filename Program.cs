using Microsoft.AspNetCore.OData;
using Microsoft.AspNetCore.OData.Query.Expressions;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using WebAPI.Models;
//using ModelBuilder = WebAPI.Models.ModelBuilder1;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<NoteContext>(x => x.UseNpgsql(builder.Configuration.GetConnectionString("DbCon")));

//builder.Services.AddControllers().
//    AddOData(opt => opt.EnableQueryFeatures()
//    .Count().Filter().OrderBy().Expand().Select().SetMaxTop(10)
//    .AddRouteComponents(
//        routePrefix: "odata",
//        model: ModelBuilder1.GetEdmModel()

//        ));

builder.Services.AddControllers();
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

app.UseAuthorization();

app.MapControllers();

app.Run();
