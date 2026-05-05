using Deneme2.BuildingBlocks.Database.Migrator;
using Deneme2.Services.Info;
using Deneme2.Services.StockService.Persistence.EntityFrameworkCore.Contexts;
using Deneme2.Services.StockService.WebApi.ServiceRegistrations;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddServiceRegistrations(builder.Environment, builder.Configuration);
builder.AddSeqEndpoint(ServiceKeys.Seq);


WebApplication app = builder.Build();

    await app.MigrateAsync<ApplicationWriteDbContext>();

app.UseServices();

await app.RunAsync();
