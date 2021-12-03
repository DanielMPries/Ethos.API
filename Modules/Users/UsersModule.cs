
public class UsersModule : ModuleBase
{
    public const string ModuleName = "Users";
    public override string Name => ModuleName;
    public override IServiceCollection RegisterModule(IServiceCollection services)
    {
        // register module items ... register ports to adapters
        return services;
    }
}