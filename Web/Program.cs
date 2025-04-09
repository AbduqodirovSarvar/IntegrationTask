using Web.Extentions;

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureServices();

var app = builder.Build();

app.ConfigurePipeline();

await app.ApplyMigration();

app.Run();
