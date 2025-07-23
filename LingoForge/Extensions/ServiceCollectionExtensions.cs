using LingoForge.Domain.DTOs.Responses;
using LingoForge.Domain.Enums;
using LingoForge.Domain.Security;
using LingoForge.Handlers;
using LingoForge.Application;
using LingoForge.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace LingoForge.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCustomServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();
        services.AddInfrastructure(configuration);
        services.AddApplication();
        services.AddRouting(options => options.LowercaseUrls = true);
        services.AddSingleton<IAuthorizationMiddlewareResultHandler, AuthorizationResultHandler>();

        return services;
    }

    public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(config =>
        {
            config.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Description = @"Atribua o seu token jwt.
                                Digite a palavra 'Bearer' e dê um espaço, logo após o espaço coloque o seu jwt.
                                Exemplo: 'Bearer 1234fc'",
                In = ParameterLocation.Header,
                Scheme = "Bearer",
                Type = SecuritySchemeType.ApiKey,
            });

            config.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header
                    },
                    new List<string>()
                }
            });
        });

        return services;
    }

    public static IServiceCollection AddAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var signingKey = configuration.GetValue<string>("Settings:Jwt:SigningKey");

        services.AddAuthentication(config =>
        {
            config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(config =>
        {
            config.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = new TimeSpan(0),
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey!)),
                RoleClaimType = ClaimTypes.Role,
                NameClaimType = ClaimTypes.Name,
            };

            config.Events = new JwtBearerEvents
            {
                OnAuthenticationFailed = context =>
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    context.Response.ContentType = "application/json";
                    var errorResponse = new BaseResponseErrorDTO("Seu token é inválido ou expirou.");
                    return context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse));
                },

                OnChallenge = context =>
                {
                    context.HandleResponse();

                    if (!context.Response.HasStarted)
                    {
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        context.Response.ContentType = "application/json";
                        var errorResponse = new BaseResponseErrorDTO("É necessário uma autenticação para acessar este recurso.");
                        return context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse));
                    }

                    return Task.CompletedTask;
                }
            };
        });

        return services;
    }

    public static IServiceCollection AddCustomAuthorization(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            options.AddPolicy(AuthorizationPolicies.MustBeStudent, policy =>
                policy.RequireRole(nameof(EUserRole.STUDENT)));

            options.AddPolicy(AuthorizationPolicies.MustBeTeacher, policy =>
                policy.RequireRole(nameof(EUserRole.TEACHER)));
        });

        return services;
    }

    public static IServiceCollection AddUseCors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("AllowAll",
                policy => policy
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
        });

        return services;
    }
}
