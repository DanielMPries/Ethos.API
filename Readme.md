This is a trial run of using a more declartive project structure using hexagonal architecture with net6 minimal APIs in an 
attempt to follow the ideals layed out by [Tim Deschryver](https://timdeschryver.dev/blog/maybe-its-time-to-rethink-our-project-structure-with-dot-net-6#conclusion)

Its just a thought experiment...

eg
```
WebApplication
│   appsettings.json
│   Program.cs
│   WebApplication.csproj
│   Module Registry
├───Modules
│   ├───SomeOtherModule
│   │      SomeOtherModule.cs
│   └───Users
│       │   UsersModule.cs
│       ├───Endpoints - API endponts
│       │       CreateUser.cs
│       │       GetUser.cs
│       ├───Core - Dtos, Models, etc.
│       │       CreateUserDto.cs
│       │       GetUserResponse.cs
│       │───Ports - Interfaces for repos and services
│       └───Adapters - Implemetations for Ports
```