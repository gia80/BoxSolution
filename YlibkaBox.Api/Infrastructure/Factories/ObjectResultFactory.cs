using System.Net;
using Microsoft.AspNetCore.Mvc;
using YlibkaBox.Api.Domain.Contracts;

namespace YlibkaBox.Api.Infrastructure.Factories;

public class ObjectResultFactory : IObjectResultFactory
{
    public ObjectResult this[HttpStatusCode code] => ObjectResult(code);

    public ObjectResult this[HttpStatusCode code, object obj] => ObjectResult(code, obj);

    public ObjectResult this[HttpStatusCode code, string message] => ObjectResult(code, message);


    private ObjectResult ObjectResult(HttpStatusCode statusCode)
    {
        return new ObjectResult(statusCode.ToString())
            { StatusCode = (int)statusCode };
    }

    private ObjectResult ObjectResult(HttpStatusCode statusCode, string message)
    {
        return new ObjectResult(message) { StatusCode = (int)statusCode };
    }

    private ObjectResult ObjectResult(HttpStatusCode statusCode, object obj)
    {
        return new ObjectResult(obj) { StatusCode = (int)statusCode };
    }
}