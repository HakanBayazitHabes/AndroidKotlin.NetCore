using AndroidKotlin.API.Models;
using Microsoft.AspNetCore.OData.Extensions;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using AndroidKotlin.Shared.Extensions;
using FluentValidation.AspNetCore;
using AndroidKotlin.Shared.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
{
    options.Authority = "http://localhost:5001";
    options.Audience = "resource_product_api";
    options.RequireHttpsMetadata = false;
});

static IEdmModel GetEdmModel()
{
    ODataConventionModelBuilder builder = new();
    builder.EntitySet<Category>("Categories");
    builder.EntitySet<Product>("Products");


    return builder.GetEdmModel();
}

builder.Services.AddControllers(options =>
{
    options.Filters.Add<ValidateModelAttribute>();
}).AddOData(opt => opt.AddRouteComponents("odata", GetEdmModel()).Filter().Select().Expand().OrderBy().SetMaxTop(null).Count()).AddFluentValidation(options =>
{
    options.RegisterValidatorsFromAssemblyContaining<Program>();
}); ;
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDelayRequestDevelopment();
    //app.UseSwagger();
    //app.UseSwaggerUI();
}

app.UseCustomException();

//var builderOdata = new ODataConventionModelBuilder();


////ProductsController
//builderOdata.EntitySet<Product>("Products");
//builderOdata.EntitySet<Category>("Categories");

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
//app.UseEndpoints(endpoints =>
//{
//    endpoints.Select().Expand().Filter().OrderBy().Count();
//    //odata/products
//    endpoints.MapODataRoute("odata", "odata", builderOdata.GetEdmModel());

//    endpoints.MapControllers();
//});

app.MapControllers();
app.Run();
