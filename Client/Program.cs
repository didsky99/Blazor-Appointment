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

// Root components
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// 🔧 Determine environment & API base URL dynamically
string apiBaseUrl;

#if DEBUG
apiBaseUrl = "https://localhost:7250/"; // local development server
#else
apiBaseUrl = builder.HostEnvironment.BaseAddress;
// For hosted Blazor WASM on Azure, API lives under the same domain (e.g. /api/)
#endif

// 🧩 Register HttpClient with dynamic base URL
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri(apiBaseUrl)
});

// Register MudBlazor services
builder.Services.AddMudServices();

// Localization
builder.Services.AddLocalization();

// Custom API access services
builder.Services.AddScoped<AppointmentService>();
builder.Services.AddScoped<CompanyService>();

await builder.Build().RunAsync();
