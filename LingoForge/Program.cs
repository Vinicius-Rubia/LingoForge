using LingoForge.Extensions;
using LingoForge.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddCustomServices(builder.Configuration)
    .AddSwaggerDocumentation()
    .AddUseCors()
    .AddCustomAuthorization()
    .AddAuthentication(builder.Configuration);

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

app.UseCors("AllowAll");

app.UseCustomSwagger(app.Environment)
    .UseHttpsRedirection()
    .UseAuthentication()
    .UseAuthorization();

app.MapControllers();

app.Run();
