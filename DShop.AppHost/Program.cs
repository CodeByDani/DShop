var builder = DistributedApplication.CreateBuilder(args);
var username = builder.AddParameter("user", secret: true);
var password = builder.AddParameter("pass", secret: true);
//TODO Tools
var postgres = builder
    .AddPostgres("postgres", username, password, 5432)
    .WithBindMount("./data/postgres", "/var/lib/postgresql/data")
    .WithPgWeb();
var catalogDb = postgres.AddDatabase("catalogDb");

//TODO Projects
builder.AddProject<Projects.Catalog_API>("catalog-api")
.WithReference(catalogDb);

builder.Build().Run();
