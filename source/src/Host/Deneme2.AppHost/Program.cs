using Deneme2.AppHost;

IDistributedApplicationBuilder builder = DistributedApplication.CreateBuilder(args);

const string
    yarpKey = "yarp",
    keycloakKey = "keycloak",
    seqKey = "seq",
    pgServerKey = "postgres-server",
    redisKey = "redis",
    rabbitmqKey = "rabbitmq",
    pgProductServiceDb = "pg-productservice",
    pgCategoryServiceDb = "pg-categoryservice",
    pgStockServiceDb = "pg-stockservice",
    productServiceKey = "productservice",
    categoryServiceKey = "categoryservice",
    stockServiceKey = "stockservice";

IResourceBuilder<KeycloakResource> keycloak = builder
    .AddKeycloak(keycloakKey)
    .WithLifetime(ContainerLifetime.Persistent)
    .WithHealthCheck()
    .WithDataVolume()
    .WithBindMount("./keycloak-realm-product.json", "/opt/keycloak/data/import/realm-product.json");


IResourceBuilder<SeqResource> seq = builder
    .AddSeq(seqKey)
    .WithDataVolume()
    .WithLifetime(ContainerLifetime.Persistent)
    .ExcludeFromManifest();

IResourceBuilder<PostgresServerResource> postgres = builder
    .AddPostgres(pgServerKey)
    .WithLifetime(ContainerLifetime.Persistent)
    .WithDataVolume()
    .WithHealthCheck()
    .WithPgAdmin();

IResourceBuilder<RedisResource> cache = builder
    .AddRedis(redisKey)
    .WithLifetime(ContainerLifetime.Persistent)
    .WithDataVolume()
    .WithHealthCheck()
    .WithRedisInsight();

IResourceBuilder<RabbitMQServerResource> rabbitmq = builder
    .AddRabbitMQ(rabbitmqKey)
    .WithLifetime(ContainerLifetime.Persistent)
    .WithHealthCheck()
    .WithDataVolume()
    .WithManagementPlugin();


IResourceBuilder<PostgresDatabaseResource> productServicePostgresDatabase = postgres.AddDatabase(pgProductServiceDb);
IResourceBuilder<ProjectResource> productService = builder
    .AddProject<Projects.Deneme2_Services_ProductService_WebApi>(productServiceKey)
    .WithHttpHealthCheck("/health")
    .WithReference(seq)
    .WithReference(cache)
    .WithReference(rabbitmq)
    .WithReference(productServicePostgresDatabase)
    .WaitFor(productServicePostgresDatabase)
    .WithReference(keycloak)
    .WaitFor(keycloak);

IResourceBuilder<PostgresDatabaseResource> categoryServicePostgresDatabase = postgres.AddDatabase(pgCategoryServiceDb);
IResourceBuilder<ProjectResource> categoryService = builder
    .AddProject<Projects.Deneme2_Services_CategoryService_WebApi>(categoryServiceKey)
    .WithHttpHealthCheck("/health")
    .WithReference(seq)
    .WithReference(cache)
    .WithReference(rabbitmq)
    .WithReference(categoryServicePostgresDatabase)
    .WaitFor(categoryServicePostgresDatabase)
    .WithReference(keycloak)
    .WaitFor(keycloak);

IResourceBuilder<PostgresDatabaseResource> stockServicePostgresDatabase = postgres.AddDatabase(pgStockServiceDb);
IResourceBuilder<ProjectResource> stockService = builder
    .AddProject<Projects.Deneme2_Services_StockService_WebApi>(stockServiceKey)
    .WithHttpHealthCheck("/health")
    .WithReference(seq)
    .WithReference(cache)
    .WithReference(rabbitmq)
    .WithReference(stockServicePostgresDatabase)
    .WaitFor(stockServicePostgresDatabase)
    .WithReference(keycloak)
    .WaitFor(keycloak);


productService
    .WithReference(categoryService)
    .WaitFor(categoryService)
    .WithReference(stockService)
    .WaitFor(stockService);



builder.AddProject<Projects.Yarp_ProxyService>(yarpKey)
    .WithReference(productService)
    .WithReference(categoryService)
    .WithReference(stockService)
    .WithReference(keycloak)
    .WithReference(seq)
    .WithExternalHttpEndpoints();

await builder.Build().RunAsync();

