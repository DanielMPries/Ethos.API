using Microsoft.AspNetCore.Mvc;

[RegisterTo(UsersModule.ModuleName)]
public class CreateUser : IModuleEndpoint {
    public void Map(IEndpointRouteBuilder endpoints) {
        endpoints.MapPut($"/{UsersModule.ModuleName}/CreateUser", 
        ([FromBody] CreateUserDto user) => new GetUserResponse {
            FirstName = user.FirstName,
            LastName = user.LastName,
            LastLogin = DateTime.UtcNow,
            UserName = user.UserName
        });
    }
}