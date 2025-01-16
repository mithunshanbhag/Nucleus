namespace Nucleus.Services;

public abstract class ServiceBase
{
    protected readonly IConfiguration Configuration;

    protected readonly ILogger<ServiceBase> Logger;

    protected readonly IMapper Mapper;

    protected ServiceBase(
        IConfiguration configuration,
        IMapper mapper,
        ILogger<ServiceBase> logger)
    {
        Configuration = configuration;
        Mapper = mapper;
        Logger = logger;
    }
}