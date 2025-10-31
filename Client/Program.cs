using BlazorAppointmentSystem.Client;
using BlazorAppointmentSystem.Client.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using System.Globalization;

var culture = new CultureInfo("id-ID");
CultureInfo.DefaultThreadCurrentCulture = culture;
CultureInfo.DefaultThreadCurrentUICulture = culture;


var builder = WebAssemblyHostBuilder.CreateDefault(args);

// Root component
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Register HttpClient (points to Server API)
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("https://localhost:7250/")
});

// Register MudBlazor services
builder.Services.AddMudServices();


builder.Services.AddLocalization();

// Register custom service for API access
builder.Services.AddScoped<AppointmentService>();
builder.Services.AddScoped<CompanyService>();

await builder.Build().RunAsync();
