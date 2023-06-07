using MediMove.Server;
using MediMove.Server.Data;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Text;
using MediMove.Server.Options;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using FluentValidation;
using MediatR;
using MediMove.Server.Behaviors;
using MediMove.Server.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.ConfigureOptions<ConfigureSwaggerOptions>();

IConfiguration config = new ConfigurationBuilder()
    .AddJsonFile("credentials.json", optional: true, reloadOnChange: true)
    .Build();

var connectionString = config.GetSection("ConnectionStrings")["MediMoveConnection"];
var authConfig = config.GetSection("Authentication");
var authenticationSettings = new AuthenticationSettings();
authConfig.Bind(authenticationSettings);

//Authentication
builder.Services.AddSingleton(authenticationSettings);

builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = "Bearer";
    option.DefaultScheme = "Bearer";
    option.DefaultChallengeScheme = "Bearer";
}).AddJwtBearer(cfg =>
{
    cfg.RequireHttpsMetadata = false;
    cfg.SaveToken = true;
    cfg.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = authenticationSettings.JwtIssuer,
        ValidAudience = authenticationSettings.JwtIssuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JwtKey))

    };
});

builder.Services.AddDbContextPool<MediMoveDbContext>(options =>
    options.UseSqlServer(connectionString));


builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

//builder.Services.AddScoped<ITeamService, TeamService>();

builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

builder.Services.AddMediatR(cfg => {
    cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
    cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
});
builder.Services.AddAutoMapper(typeof(MediMoveMappingProfile));

// Swagger
builder.Services.AddSwaggerGen(options =>
{
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

    var securityScheme = new OpenApiSecurityScheme
    {
        Name = "Authorization",
        BearerFormat = "JWT",
        Scheme = "bearer",
        Description = "Specify the authorization token.",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http
    };
    options.AddSecurityDefinition("Bearer", securityScheme);
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
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
    app.UseExceptionHandler("/api/Error/Development");
}
else
{
    app.UseExceptionHandler("/api/Error");

    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// Swagger
var provider = app.Services.GetService<IApiVersionDescriptionProvider>();
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    foreach (var description in provider.ApiVersionDescriptions)
        options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName);
});
app.UseApiVersioning();
app.UseAuthentication();
app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

app.MapRazorPages();

app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
