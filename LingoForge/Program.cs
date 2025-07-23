using LingoForge.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddCustomServices(builder.Configuration)
    .AddSwaggerDocumentation()
    .AddUseCors()
    .AddCustomAuthorization()
    .AddAuthentication(builder.Configuration);

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

app.UseCors("AllowAll");

app.UseCustomSwagger(app.Environment)
    .UseHttpsRedirection()
    .UseAuthentication()
    .UseAuthorization();

app.MapControllers();

app.Run();
