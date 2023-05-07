using MediMove.Server;
using MediMove.Server.Data;
using MediMove.Server.Repositories;
using MediMove.Server.Repositories.Contracts;
using MediMove.Server.Services.PatientService;
using MediMove.Server.Services.TransportService;
using MediMove.Server.Services.AvailabilityService;
using MediMove.Server.Services.ParamedicService;
using MediMove.Server.Services.TeamService;
using Microsoft.EntityFrameworkCore;
using MediMove.Server.Services.DispatcherService;
using System.Reflection;
using MediMove.Server.Options;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.ConfigureOptions<ConfigureSwaggerOptions>();

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

// Swagger
builder.Services.AddSwaggerGen(options =>
{
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

builder.Services.AddApiVersioning(options =>
{
    options.ReportApiVersions = true;
    options.AssumeDefaultVersionWhenUnspecified = true;
});

builder.Services.AddVersionedApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
    

}
else
{

    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseExceptionHandler("/api/Error");
// Swagger
var provider = app.Services.GetService<IApiVersionDescriptionProvider>();
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    foreach (var description in provider.ApiVersionDescriptions)
        options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
});
app.UseApiVersioning();

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();


app.MapRazorPages();

app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
