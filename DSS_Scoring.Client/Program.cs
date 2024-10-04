using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

await builder.Build().RunAsync();

//Abe 
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7243/") });

builder.Services.AddBlazorBootstrap();