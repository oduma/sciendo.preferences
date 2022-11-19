using sciendo.preferences.service.Logic;
using sciendo.preferences.service.Services;
using Sciendo.Last.Fm;

var apiKey = "6c66a3b570ea6b98abeebe0c1e9323c7";
var builder = WebApplication.CreateBuilder(args);

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddGrpcReflection();
builder.Services.AddLogging();
builder.Services.Add(new ServiceDescriptor(typeof(IFilterLocally), typeof(FilterLocally),
    ServiceLifetime.Transient));
builder.Services.Add(new ServiceDescriptor(typeof(ILastFmGetter), typeof(LastFmGetter), 
    ServiceLifetime.Transient));
builder.Services.AddTransient<IUrlProvider>(s=>
new UrlProvider(s.GetRequiredService<ILogger<UrlProvider>>(),apiKey));


var app = builder.Build();

IWebHostEnvironment env = app.Environment;

if(env.IsDevelopment())
{
    app.MapGrpcReflectionService();
}

// Configure the HTTP request pipeline.
app.MapGrpcService<PreferencesService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
