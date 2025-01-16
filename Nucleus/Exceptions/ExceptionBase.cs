namespace Nucleus.Exceptions;

public abstract class ExceptionBase : Exception
{
    protected ExceptionBase(string message) : base(message)
    {
    }

    protected ExceptionBase(string message, Exception innerException) : base(message, innerException)
    {
    }

    public abstract IActionResult ToActionResult();

    public abstract HttpResponseMessage ToHttpResponseMessage();
}