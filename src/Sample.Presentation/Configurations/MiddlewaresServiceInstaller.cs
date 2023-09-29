using Sample.Presentation.Middlewares;

namespace Sample.Presentation.Configurations;

public class MiddlewaresServiceInstaller : IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<GlobalExceptionHandlingMiddleware>();
    }
}