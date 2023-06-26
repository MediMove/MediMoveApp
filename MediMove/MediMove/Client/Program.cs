using MediMove.Client;
using MediMove.Client.Services;
using MediMove.Client.temp;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });


builder.Services.AddScoped<MediMoveAuthenticationStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(sp => sp.GetRequiredService<MediMoveAuthenticationStateProvider>());
//builder.Services.AddScoped<ErrorResponse>();
builder.Services.AddScoped<TransportService>();
builder.Services.AddScoped<PatientService>();
builder.Services.AddScoped<TeamService>();
builder.Services.AddScoped<AvailabilityService>();
builder.Services.AddScoped<EmployeeService>();
builder.Services.AddScoped<ParamedicService>();
builder.Services.AddAuthorizationCore();

await builder.Build().RunAsync();
