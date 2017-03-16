dotnet ef database drop -c SchoolContext
dotnet ef migrations add InitialCreate -c SchoolContext
dotnet ef database update -c SchoolContext

