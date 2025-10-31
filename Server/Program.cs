using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Components.WebAssembly.Server;
using BlazorAppointmentSystem.Server.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorClient",
        policy =>
        {
            policy.WithOrigins("http://localhost:5062", "https://localhost:5062") // client URLs
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

// 🩶 1️⃣ Add DbContext
builder.Services.AddDbContext<AppointmentDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseSqlServer(connectionString);
});

// 🩶 2️⃣ Add Controllers & Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 🩶 3️⃣ Add Razor + Blazor
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

var app = builder.Build();

// ✅ Enable CORS before MapControllers
app.UseCors("AllowBlazorClient");

app.UseHttpsRedirection();
app.UseAuthorization();

// 🩶 4️⃣ Middleware setup
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();
app.UseBlazorFrameworkFiles();   // ✅ Important for Blazor WASM

app.UseRouting();

app.MapControllers();
//app.MapBlazorHub();
app.MapFallbackToFile("/_Host");

app.Run();
