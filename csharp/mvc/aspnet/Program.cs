var builder = WebApplication.CreateBuilder(args);

// Use the Startup class
var startup = new Startup(builder.Configuration);
startup.ConfigureServices(builder.Services);

var app = builder.Build();

// Use Startup for middleware configuration
var env = app.Environment;
startup.Configure(app, env);

app.Run();
