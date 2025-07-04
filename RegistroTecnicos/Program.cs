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

//Inyeccion del contexto
//Contexto
var ConString = builder.Configuration.GetConnectionString("ConStr");
builder.Services.AddDbContextFactory<Contexto>(o => o.UseSqlServer(ConString));

builder.Services.AddBlazorBootstrap();

//Inyeccion del service
builder.Services.AddScoped<TecnicosService>();

//Inyeccion del service de cliente 
builder.Services.AddScoped<ClientesService>();

//Inyeccion del service de tickets
builder.Services.AddScoped<TicketsService>();

//Inyeccion del service de sistemas 
builder.Services.AddScoped<SistemasService>();

builder.Services.AddScoped<VentasService>();







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
