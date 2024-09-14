var builder = DistributedApplication.CreateBuilder(args);
var userCatalog = builder.AddParameter("User", true);
var passCatalog = builder.AddParameter("Pass", true);

//TODO Tools
var postgres = builder
    .AddPostgres("postgres", userCatalog, passCatalog, 5432)
    .WithBindMount("./data/CatalogDb", "/var/lib/postgresql/data")
    .WithPgWeb();
var catalogDb = postgres.AddDatabase("CatalogDb");
//TODO Projects
builder.AddProject<Projects.Catalog_API>("catalog-api")
.WithReference(catalogDb);

builder.Build().Run();
