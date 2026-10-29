using DSW_3P_PR01.Services;
using DSW_3P_PR01.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOpenApi();

//Agregar controladores
builder.Services.AddControllers();

//Agregar dependencias
builder.Services.AddScoped<IMisionRepository, MisionJsonRepository>();
builder.Services.AddScoped<IMisionService, MisionService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint(
            "/openapi/v1.json",
            "API de Misiones v1"
        );
    });
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();

