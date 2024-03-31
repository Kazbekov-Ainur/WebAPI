using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using WebAPI.Models;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<NoteContext>(x => x.UseNpgsql(builder.Configuration.GetConnectionString("DbCon")));

builder.Services.AddControllers().
    AddOData(opt => opt.EnableQueryFeatures()
    .Count().Filter().OrderBy().Expand().Select().SetMaxTop(10)
    .AddRouteComponents(
        routePrefix: "odata",
        model: GetEdmModel()

        ));


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


static IEdmModel GetEdmModel()
{
	var builder = new ODataConventionModelBuilder();
	builder.EntityType<Note>();
	builder.EntitySet<Note>("Notes");
	
    return builder.GetEdmModel();
}

app.UseAuthorization();

app.MapControllers();

app.Run();