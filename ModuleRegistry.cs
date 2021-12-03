public interface IModule {

    string Name { get; }
    IServiceCollection RegisterModule(IServiceCollection services);
    IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints);
}

public interface IModuleEndpoint {
    void Map(IEndpointRouteBuilder endpoints);
}

[AttributeUsage(AttributeTargets.Class)]
public class RegisterToAttribute : Attribute {
    public string ModuleName { get; }

    public RegisterToAttribute(string moduleName) => ModuleName = moduleName;
}

public class ModuleBase : IModule
{
    public virtual string Name { get; } = "";
    public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints) {
        var moduleEndpoints = DiscoverEndpoints();
        moduleEndpoints.ForEach(endpoint => endpoint.Map(endpoints));
        return endpoints;
    }

    public virtual IServiceCollection RegisterModule(IServiceCollection services) => services;

    private IEnumerable<IModuleEndpoint> DiscoverEndpoints() {
        var returnValue = new List<IModuleEndpoint>();

        var endpoints = typeof(IModuleEndpoint).Assembly
            .GetTypes()
            .Where(p => p.IsClass && p.IsAssignableTo(typeof(IModuleEndpoint)) && p.GetCustomAttributes(typeof(RegisterToAttribute), false).Any())
            .Select(Activator.CreateInstance)
            .Cast<IModuleEndpoint>();

        endpoints.ForEach( e => {
            var attr = e.GetType().GetCustomAttributes(typeof(RegisterToAttribute), false).FirstOrDefault() as RegisterToAttribute;
            if( attr?.ModuleName == Name ) {
                returnValue.Add(e);
            }
        });

        return returnValue;
    }
}

public static class ModuleExtensions {
    private static readonly List<IModule> registry = new();

    public static WebApplicationBuilder RegisterModules(this WebApplicationBuilder builder) {
        var modules = DiscoverModules();
        modules.ForEach(module => {
            module.RegisterModule(builder.Services);
            registry.Add(module);
        });
        return builder;
    }

    public static IEndpointRouteBuilder MapEndpoints(this IEndpointRouteBuilder builder) {
        registry.ForEach(module => module.MapEndpoints(builder));
        return builder;
    }

    private static IEnumerable<IModule> DiscoverModules() =>
         typeof(IModule).Assembly
            .GetTypes()
            .Where(p => p.IsClass && p.IsAssignableTo(typeof(IModule)))
            .Select(Activator.CreateInstance)
            .Cast<IModule>();
}