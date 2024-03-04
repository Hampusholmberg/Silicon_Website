using Infrastructure.Models;

namespace Infrastructure.Factories;

public class ResponseFactory
{
    public static ResponseResult Ok()
    {
        return new ResponseResult
        {
            Message = "Succeeded",
            StatusCode = StatusCode.OK,
        };
    }
    public static ResponseResult Ok(string? message = null)
    {
        return new ResponseResult
        {
            Message = message ?? "Succeeded",
            StatusCode = StatusCode.OK,
        };
    }
    public static ResponseResult Ok(object obj, string? message = null)
    {
        return new ResponseResult
        {
            Message = message ?? "Succeeded",
            StatusCode = StatusCode.OK,
            ContentResult = obj
        };
    }

    public static ResponseResult Error(string? message = null)
    {
        return new ResponseResult
        {
            Message = message ?? "Failed",
            StatusCode = StatusCode.ERROR,
        };
    }

    public static ResponseResult NotFound(string? message = null)
    {
        return new ResponseResult
        {
            Message = message ?? "Not found",
            StatusCode = StatusCode.NOT_FOUND,
            Exists = false
        };
    }

    public static ResponseResult Exists(string? message = null)
    {
        return new ResponseResult
        {
            Message = message ?? "Already exists",
            StatusCode = StatusCode.EXISTS,
            Exists = true

        };
    }
}
