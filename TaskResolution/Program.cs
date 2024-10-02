using AutoMapper;
using Castle.Windsor;
using Castle.Windsor.MsDependencyInjection;
using log4net;
using log4net.Config;
using Microsoft.EntityFrameworkCore;
using TaskResolution.Application.Core.Contracts;
using TaskResolution.Application.Core.Services;
using TaskResolution.Configuration;
using TaskResolution.Loggers;
using TaskResolution.Repositories.Core;
using TaskResolution.Repositories.Core.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Load Log4Net configuration
var logRepository = LogManager.GetRepository(System.Reflection.Assembly.GetEntryAssembly());
XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));

// Setup Windsor container
var container = new WindsorContainer();
container.Install(new WindsorInstaller());

// Integrate Castle Windsor with .NET 8 DI
builder.Host.UseServiceProviderFactory(new WindsorServiceProviderFactory());

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<StudentsDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("StudentsConnectionString")));

// Register custom ILogger with Log4Net
builder.Services.AddTransient<TaskResolution.Loggers.ILogger>(provider => new Log4NetLogger(typeof(Program)));

builder.Services.AddScoped<IStudentsService, StudentsService>();
builder.Services.AddScoped<IStudentsRepository, StudentsRepository>();
builder.Services.AddScoped<ISubjectsService, SubjectsService>();
builder.Services.AddScoped<ISubjectsRepository, SubjectsRepository>();

var config = new MapperConfiguration(mc =>
{
    mc.AddProfile(new TaskResolution.Application.Core.Profiles.CoreMappingProfile());
    mc.AddProfile(new TaskResolution.Repositories.Core.Profiles.CoreMappingProfile());
});
IMapper mapper = config.CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "TaskResolution Student APIs",
        Version = "v1",
        Description = "API documentation for TaskResolution Student services."
    });

    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = System.IO.Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (System.IO.File.Exists(xmlPath))
    {
        c.IncludeXmlComments(xmlPath);
    }
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
