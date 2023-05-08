using AndroidKotlin.API.Models;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddOData();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.UseSwagger();
    //app.UseSwaggerUI();
}

app.UseAuthorization();

var builderOdata = new ODataConventionModelBuilder();

//ProductsController
builderOdata.EntitySet<Product>("Products");
builderOdata.EntitySet<Category>("Categories");


app.UseEndpoints(endpoints =>
{
    endpoints.Select().Expand().Filter().OrderBy().Count();
    //odata/products
    endpoints.MapODataRoute("odata", "odata", builderOdata.GetEdmModel());

    endpoints.MapControllers();
});


app.MapControllers();

app.Run();
