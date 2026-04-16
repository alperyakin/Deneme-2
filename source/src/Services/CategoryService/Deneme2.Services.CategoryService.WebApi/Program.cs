using Deneme2.BuildingBlocks.Database.Migrator;
using Deneme2.Services.CategoryService.Persistence.EntityFrameworkCore.Contexts;
using Deneme2.Services.CategoryService.WebApi.ServiceRegistrations;
using Deneme2.Services.Info;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddServiceRegistrations(builder.Environment, builder.Configuration);
builder.AddSeqEndpoint(ServiceKeys.Seq);

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
    await app.MigrateAsync<ApplicationWriteDbContext>();

app.UseServices();

await app.RunAsync();
