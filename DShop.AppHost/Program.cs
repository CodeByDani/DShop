var builder = DistributedApplication.CreateBuilder(args);

//TODO Tools
var catalogDb = builder.AddPostgres("catalogDb").WithPgAdmin();

//TODO Projects
builder.AddProject<Projects.Catalog_API>("catalog-api")
.WithReference(catalogDb);

builder.Build().Run();
