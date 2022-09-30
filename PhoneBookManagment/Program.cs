using Microsoft.OpenApi.Models;
using PhoneBookManagment.BLL.RepositoryService.Implementation;
using PhoneBookManagment.BLL.RepositoryService.Interface;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(optios =>
{
    optios.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Phone Book API",
        License = new OpenApiLicense
        {
            Name = "Web Api created by Klevis Mema",
            Url = new Uri("https://www.linkedin.com/in/klevis-m-ab1b3b140/")
        }
    });
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    optios.IncludeXmlComments(xmlPath);
});

builder.Services.AddTransient<IPhoneBookRepositoryService, PhoneBookRepositoryService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<ITypeService, TypeService>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
