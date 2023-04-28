using System.Collections.Immutable;
using System.Data.Common;
using AutoMapper;
using MediMove.Server;
using MediMove.Server.Data;
using MediMove.Server.Middleware;
using MediMove.Server.Repositories;
using MediMove.Server.Repositories.Contracts;
using MediMove.Server.Services.PatientService;
using MediMove.Server.Services.TransportService;
using MediMove.Server.Services.AvailabilityService;
using MediMove.Server.Services.ParamedicService;
using MediMove.Server.Services.TeamService;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using MediMove.Server.Services.DispatcherService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
});

IConfiguration config = new ConfigurationBuilder()
    .AddJsonFile("credentials.json", optional: true, reloadOnChange: true)
    .Build();

var connectionString = config.GetSection("ConnectionStrings")["MediMoveConnection"];

builder.Services.AddDbContextPool<MediMoveDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped<ITransportRepository, TransportRepository>();
builder.Services.AddScoped<ITransportService, TransportService>();

builder.Services.AddScoped<IPatientRepository, PatientRepository>();
builder.Services.AddScoped<IPatientService, PatientService>();

builder.Services.AddScoped<IPersonalInformationRepository, PersonalInformationRepository>();

builder.Services.AddScoped<IParamedicRepository, ParamedicRepository>();
builder.Services.AddScoped<IAvailabilityRepository, AvailabilityRepository>();
builder.Services.AddScoped<IAvailabilityService, AvailabilityService>();

builder.Services.AddScoped<IParamedicService, ParamedicService>();

builder.Services.AddScoped<IDispatcherRepository, DispatcherRepository>();
builder.Services.AddScoped<IDispatcherService, DispatcherService>();

builder.Services.AddScoped<ITeamRepository, TeamRepository>();
builder.Services.AddScoped<ITeamService, TeamService>();

builder.Services.AddAutoMapper(typeof(MediMoveMappingProfile));
builder.Services.AddScoped<ErrorHandlingMiddleware>();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
    
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
});

app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();


app.MapRazorPages();

app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
