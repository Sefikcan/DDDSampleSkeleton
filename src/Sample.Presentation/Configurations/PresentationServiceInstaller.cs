namespace Sample.Presentation.Configurations;

public class PresentationServiceInstaller : IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();

        services.AddSwaggerGen();
    }
}