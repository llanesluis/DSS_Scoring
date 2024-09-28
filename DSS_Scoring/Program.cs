using System.Text.RegularExpressions;
using DSS_Scoring.Client.Pages;
using DSS_Scoring.Components;
using Microsoft.AspNetCore.Http.HttpResults;

using Microsoft.EntityFrameworkCore;
using DSS_Scoring.Data;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();
builder.Services.AddBlazorBootstrap();
// Agregar soporte para endpoint de API y rutas en Blazor
builder.Services.AddEndpointsApiExplorer();

// Add the database context to the container. *using a PostgreSQL connection string*
builder.Services.AddDbContext<MyDbContext>(options => {
    //var connectionString = builder.Configuration.GetConnectionString("SupabaseConnection");
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseNpgsql(connectionString);
});

// Add the controllers to the services collection
builder.Services.AddControllers().AddControllersAsServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(DSS_Scoring.Client._Imports).Assembly);

// Manera moderna de mapear los controladores 
app.MapControllers();

// ASP.NET Core 8.0 ya no usa app.UseEndpoints() de la misma manera que antes.
//app.UseEndpoints((endpoints) =>
//{
//    app.MapControllers();
//});

app.Run();
