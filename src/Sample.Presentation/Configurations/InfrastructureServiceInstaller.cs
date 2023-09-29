using Microsoft.EntityFrameworkCore;
using Sample.Domain.Repositories;
using Sample.Infrastructure;
using Sample.Infrastructure.Interceptors;
using Sample.Infrastructure.Repositories;

namespace Sample.Presentation.Configurations;

public class InfrastructureServiceInstaller : IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(
            (sp, optionsBuilder) =>
            {
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("Database"),
                    x => x.EnableRetryOnFailure());

                optionsBuilder.AddInterceptors(
                    sp.GetRequiredService<ConvertDomainEventsToOutboxMessagesInterceptor>(),
                    sp.GetRequiredService<UpdateAuditableEntitiesInterceptor>());
            });
        
        services.AddScoped<IUnitOfWork>(serviceProvider => serviceProvider.GetRequiredService<ApplicationDbContext>());
        
        services.AddScoped<ConvertDomainEventsToOutboxMessagesInterceptor>();
        services.AddScoped<UpdateAuditableEntitiesInterceptor>();
        
        services.AddTransient<IMemberRepository, MemberRepository>();
        services.AddTransient<IGatheringRepository, GatheringRepository>();
    }
}