using Blazored.Toast;
using Microsoft.EntityFrameworkCore;
using RegistroTecnicos.Components;
using RegistroTecnicos.DAL;
using RegistroTecnicos.Services;

var builder = WebApplication.CreateBuilder(args);
// Inyeccion del servicio Toast 
builder.Services.AddBlazoredToast();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
//Inyeccion del contexto
builder.Services.AddDbContextFactory<Contexto>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddBlazorBootstrap();

//Inyeccion del service
builder.Services.AddScoped<TecnicosService>();

//Inyeccion del service de cliente 
builder.Services.AddScoped<ClientesService>();

//Inyeccion del service de tickets
builder.Services.AddScoped<TicketsService>();







var app = builder.Build();



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
