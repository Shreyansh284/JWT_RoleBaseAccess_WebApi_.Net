using Application;
using Core;
using Infrastructure;
using Microsoft.AspNetCore.Http;

namespace RoleBaseAuthUser;

public static class DependencyInjection
{
    public static IServiceCollection AddAppDI(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpContextAccessor().AddApplicationDI().AddInfrastructureDI().AddCoreDI(configuration);

        return services;
    }
}