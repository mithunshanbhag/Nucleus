namespace Nucleus.Controllers;

public abstract class ControllerBase
{
    protected readonly ILogger<ControllerBase> Logger;

    protected readonly IMediator Mediator;

    protected ControllerBase(IMediator mediator, ILogger<ControllerBase> logger)
    {
        Mediator = mediator;
        Logger = logger;
    }

    protected async Task<IActionResult> ProcessRequestAsync(IRequest<IActionResult> request)
    {
        try
        {
            return await Mediator.Send(request);
        }
        catch (ExceptionBase cpe)
        {
            return cpe.ToActionResult();
        }
        catch (ValidationException ve)
        {
            return new BadRequestObjectResult(ve.Message);
        }
    }

    protected static async Task<IActionResult> ProcessAsync(Func<Task<IActionResult>> func)
    {
        try
        {
            return await func();
        }
        catch (ExceptionBase cpe)
        {
            return cpe.ToActionResult();
        }
        catch (ValidationException ve)
        {
            return new BadRequestObjectResult(ve.Message);
        }
    }
}