using System.Collections.Immutable;
using System.Data.Common;
using MediMove.Server.Data;
using MediMove.Server.Repositories;
using MediMove.Server.Repositories.Contracts;
using MediMove.Server.Services.TransportService;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

IConfiguration config = new ConfigurationBuilder()
    .AddJsonFile("credentials.json", optional: true, reloadOnChange: true)
    .Build();

var connectionString = config.GetSection("ConnectionStrings")["MediMoveConnection"];

builder.Services.AddDbContextPool<MediMoveDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped<ITransportRepository, TransportsRepository>(); // Nie powinno byæ TransportRepository w liczbie pojedynczej?
builder.Services.AddScoped<ITransportService, TransportService>();

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

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();


app.MapRazorPages();

app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
