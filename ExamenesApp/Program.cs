using ExamenesApp;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Components.Authorization;
using ExamenesApp.Servicios;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// HttpClient apuntando al backend
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("https://localhost:7252/api/") // tu backend
});

builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
builder.Services.AddScoped<AutenticacionService>();
builder.Services.AddScoped<PerfilService>();
builder.Services.AddScoped<InscripcionExamenService>();


// Autorización
builder.Services.AddAuthorizationCore();

await builder.Build().RunAsync();
