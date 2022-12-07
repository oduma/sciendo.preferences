using Microsoft.Data.Sqlite;
using sciendo.preferences.service.Infrastructure;
using sciendo.preferences.service.Logic;
using sciendo.preferences.service.Services;
using Sciendo.Last.Fm;
using Sciendo.Web;
using System.Data;

var apiKey = "6c66a3b570ea6b98abeebe0c1e9323c7";
var connectionString = "Data Source=C:\\Code\\Sciendo\\Sciendo.Preferences\\sciendo.preferences\\sciendo.preferences\\db\\clementine.db;";
var builder = WebApplication.CreateBuilder(args);

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddGrpcReflection();
builder.Services.AddCors(o => o.AddPolicy("AllowLocalhost", b =>
{
    b.WithOrigins("https://localhost:4200")
    .WithHeaders("Grpc-Status",
        "Grpc-Message",
        "Grpc-Encoding",
        "Grpc-Accept-Encoding",
        "X-Grpc-Web",
        "User-Agent",
        "Access-Control-Allow-Origin",
        "Access-Control-Allow-Methods");
    //SetIsOriginAllowed(Helper.AllowMyClient)
    //.AllowAnyOrigin()
    //.AllowAnyMethod()
    //.AllowAnyHeader()
    //.WithExposedHeaders(
    //    "Grpc-Status",
    //    "Grpc-Message",
    //    "Grpc-Encoding",
    //    "Grpc-Accept-Encoding",
    //    "X-Grpc-Web",
    //    "User-Agent");
}));
builder.Services.AddLogging();
builder.Services.Add(new ServiceDescriptor(typeof(IFilterLocally), typeof(FilterLocally),
    ServiceLifetime.Transient));
builder.Services.Add(new ServiceDescriptor(typeof(ILastFmGetter), typeof(LastFmGetter), 
    ServiceLifetime.Transient));
builder.Services.AddTransient<IUrlProvider>(s=>
new UrlProvider(s.GetRequiredService<ILogger<UrlProvider>>(),apiKey));
builder.Services.AddTransient<IWebGet<string>>(s=>
new WebStringGet(s.GetRequiredService<ILogger<WebStringGet>>()));
builder.Services.AddTransient<IContentProvider<Temperatures>>(s =>
new ContentProvider<Temperatures>(s.GetRequiredService<ILogger<ContentProvider<Temperatures>>>(), 
s.GetRequiredService<IUrlProvider>(), 
s.GetRequiredService<IWebGet<string>>()));


builder.Services.AddTransient<IDbConnection>(s => new SqliteConnection(connectionString));
builder.Services.Add(new ServiceDescriptor(typeof(IRepository), typeof(Repository), ServiceLifetime.Transient));


var app = builder.Build();

IWebHostEnvironment env = app.Environment;

if(env.IsDevelopment())
{
    app.MapGrpcReflectionService();
}

app.UseRouting();
app.UseGrpcWeb();
app.UseCors();

app.UseEndpoints(
    e => e.MapGrpcService<PreferencesService>()
    .EnableGrpcWeb()
    .RequireCors("AllowLocalhost")

    );
// Configure the HTTP request pipeline.
//app.MapGrpcService<PreferencesService>();
//app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
