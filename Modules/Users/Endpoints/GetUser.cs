[RegisterTo(UsersModule.ModuleName)]
public class GetUser : IModuleEndpoint {
    public void Map(IEndpointRouteBuilder endpoints) {
        endpoints.MapGet($"/{UsersModule.ModuleName}/GetUser", () => new GetUserResponse {
            FirstName = "Rick",
            LastName = "Astley",
            LastLogin = DateTime.Parse("2021/10/21 11:45:00 AM"),
            UserName = "NvrGvYouUp"
        });
    }
}