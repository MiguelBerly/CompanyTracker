namespace CompanyTracker.Api.Exceptions;

public class ApiException : Exception
{
    public int StatusCode { get; }

    public ApiException(int statusCode, string message) : base(message)
    {
        StatusCode = statusCode;
    }
}

public class AllAppliedException()
    : ApiException(StatusCodes.Status404NotFound, "You have applied to all current companies.");

public class ObjectNotFoundException(int searchId) 
    : ApiException(StatusCodes.Status404NotFound, "Object not found")
{
    public int SearchId { get; } = searchId;
}

public class BadRequestException(string message) 
    : ApiException(StatusCodes.Status400BadRequest, message);


