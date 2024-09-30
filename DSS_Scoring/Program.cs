using System.Reflection;
using DSS_Scoring.Client.Pages;
using DSS_Scoring.Components;
using DSS_Scoring.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();
builder.Services.AddBlazorBootstrap();

// Agregar soporte para endpoint de API y rutas en Blazor
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<MyDbContext>(options => {
    var connectionString = builder.Configuration.GetConnectionString("SUPABASE_CONNECTION");
    options.UseNpgsql(connectionString);
});

// Add the controllers to the services collection
builder.Services.AddControllers().AddControllersAsServices();

// Add the Swagger services
builder.Services.AddSwaggerGen(c => { 
    c.SwaggerDoc("v1", new() { Title = "DSS_Scoring", 
        Version = "v1",
        Description = "API DSS Scoring"
    });

    //var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}";
    //var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    //c.IncludeXmlComments(xmlPath);
});

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();

    // Enable middleware to serve generated Swagger as a JSON endpoint.
    app.UseSwagger();
    app.UseSwaggerUI(c => { 
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "DSS_Scoring v1");
    });
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

app.Run();
