using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson(options =>
{

});

builder.Services.AddDapper(options =>
{
    options.ConnectionString = builder.Configuration.GetConnectionString("MySqlConnectionString");
    options.DatabaseType = DatabaseType.MySql;
});

// Register the Swagger generator, defining one or more Swagger documents
// https://docs.microsoft.com/zh-cn/aspnet/core/tutorials/getting-started-with-swashbuckle
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API - V1", Version = "v1" });

    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "comments.xml"), true);
});
builder.Services.AddSwaggerGenNewtonsoftSupport();

var app = builder.Build();

// Configure the HTTP request pipeline.
// Enable middleware to serve generated Swagger as a JSON endpoint.
app.UseSwagger();

// Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
});

app.UseAuthorization();

app.MapControllers();

app.Run();
