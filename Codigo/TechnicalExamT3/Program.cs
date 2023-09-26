using Business.BusinessLogic;
using Business.DataAccess.ContextDB;
using Business.DataAccess.Factory;
using Business.DataAccess.Mappers;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

#region Inyeccion dependencias propias
//Mappers de BD
builder.Services.AddSingleton<IEntityTypeMap, UserMapper>();

//Conexion a BD
var dbContextOptions = new DbContextOptionsBuilder<TechnicalExamDBContext>()
    .UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"], sqlServerOptionsAction =>
    {
        // Habilitar la resiliencia a errores transitorios
        sqlServerOptionsAction.EnableRetryOnFailure();
    })
    .Options;

builder.Services.AddSingleton(dbContextOptions);
builder.Services.AddSingleton<TechnicalExamContextOptions>();
builder.Services.AddSingleton<IContextDBFactory, ContextDBFactory>();
builder.Services.AddSingleton<TechnicalExamDBContext>();

// Configurar Firebase
//var firebaseCredentials = GoogleCredential.FromFile("ruta-a-tu-archivo-de-credenciales.json");
//FirebaseApp.Create(new AppOptions
//{
//    Credential = firebaseCredentials,
//});

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=User}/{action=Index}/{id?}");

app.Run();
