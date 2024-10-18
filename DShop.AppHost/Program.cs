var builder = DistributedApplication.CreateBuilder(args);
var userCatalog = builder.AddParameter("User", true);
var passCatalog = builder.AddParameter("Pass", true);

//TODO Tools
// Postgres
var postgres = builder
    .AddPostgres("postgres", userCatalog, passCatalog, 5432)
    .WithBindMount("./data/CatalogDb", "/var/lib/postgresql/data")
    .WithPgWeb();
var catalogDb = postgres.AddDatabase("CatalogDb");

//mongo
var basketDb = builder.AddMongoDB("mongo", 27017)
    .WithBindMount("./data/BasketDb", "/data/db")
    .AddDatabase("Basket");

//redis 
var cache = builder.AddRedis("cache", 6379)
    .WithImage("ghcr.io/microsoft/garnet")
    .WithImageTag("latest");

//TODO Projects
builder.AddProject<Projects.Catalog_API>("catalog-api")
.WithReference(catalogDb);

builder.AddProject<Projects.Basket_API>("basket-api")
    .WithReference(basketDb)
    .WithReference(cache);

builder.Build().Run();
