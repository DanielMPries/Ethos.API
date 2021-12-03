var builder = WebApplication.CreateBuilder(args);

builder.RegisterModules();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen( setup => setup.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo(){
    Description = "Microservice minimal api based on Hexogonal Architecture",
    Title = "Ethos Engine",
    Version = "v1"
}));
var app = builder.Build();

if(app.Environment.IsDevelopment()) {
    app.UseDeveloperExceptionPage();
}

app.UseSwagger();
app.MapGet("/", () => "Hello World!");
app.MapEndpoints();

app.UseSwaggerUI();
app.Run();
