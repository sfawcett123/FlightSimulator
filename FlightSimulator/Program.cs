using Listener.Hubs;
using Listener.Workers;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddRazorPages();

builder.Services.AddSignalR();

builder.Services.AddSingleton<BoardFactory>();
builder.Services.AddSingleton<SimulatorFactory>();

builder.Services.AddHostedService<BoardFactory>(provider => provider.GetRequiredService<BoardFactory>());
builder.Services.AddHostedService<SimulatorFactory>(provider => provider.GetRequiredService<SimulatorFactory>());

builder.Services.AddSwaggerGen(options =>
{
    var filePath = Path.Combine(System.AppContext.BaseDirectory, "FlightSimulator.xml");
    options.IncludeXmlComments(filePath);

   // filePath = Path.Combine(System.AppContext.BaseDirectory, "BoardManager.xml");
   // options.IncludeXmlComments(filePath);

    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Flight Simulator Listener API",
        Description = "An ASP.NET Core Web API for managing Flight Simulator Listener items",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "Example Contact",
            Url = new Uri("https://example.com/contact")
        },
        License = new OpenApiLicense
        {
            Name = "Example License",
            Url = new Uri("https://example.com/license")
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseSwagger(c =>
{
    c.SerializeAsV2 = true;
});
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapHub<FlightSimulatorHub>("/FlightSimulator");

app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
