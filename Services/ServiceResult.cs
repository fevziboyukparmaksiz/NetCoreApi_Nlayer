using System.Net;
using System.Text.Json.Serialization;

namespace Services;
public class ServiceResult<T>
{
    public T? Data { get; set; }
    public List<string>? ErrorMessage { get; set; }

    [JsonIgnore]
    public bool IsSuccess => ErrorMessage == null || ErrorMessage.Count == 0;

    [JsonIgnore]
    public bool IsFail => !IsSuccess;

    [JsonIgnore]
    public HttpStatusCode HttpStatusCode { get; set; }

    [JsonIgnore]
    public string? UrlAsCreated { get; set; }

    //static factory method
    public static ServiceResult<T> Success(T data, HttpStatusCode httpStatusCode = HttpStatusCode.OK)
    {
        return new ServiceResult<T>()
        {
            Data = data,
            HttpStatusCode = httpStatusCode
        };
    } 
    public static ServiceResult<T> SuccessAsCreated(T data, string urlAsCreated)
    {
        return new ServiceResult<T>()
        {
            Data = data,
            HttpStatusCode = HttpStatusCode.Created,
            UrlAsCreated = urlAsCreated
        };
    }

    public static ServiceResult<T> Fail(List<string> errorMessages,HttpStatusCode httpStatusCode = HttpStatusCode.BadRequest)
    {
        return new ServiceResult<T>()
        {
            ErrorMessage = errorMessages,
            HttpStatusCode = httpStatusCode
        };
    }

    public static ServiceResult<T> Fail(string errorMessage, HttpStatusCode httpStatusCode = HttpStatusCode.BadRequest)
    {
        return new ServiceResult<T>()
        {
            ErrorMessage = [errorMessage],
            HttpStatusCode = httpStatusCode
        };
    }
}

public class ServiceResult
{
    public List<string>? ErrorMessage { get; set; }

    [JsonIgnore]
    public bool IsSuccess => ErrorMessage == null || ErrorMessage.Count == 0;

    [JsonIgnore]
    public bool IsFail => !IsSuccess;

    [JsonIgnore]
    public HttpStatusCode HttpStatusCode { get; set; }

    //static factory method
    public static ServiceResult Success(HttpStatusCode httpStatusCode = HttpStatusCode.OK)
    {
        return new ServiceResult
        {
            HttpStatusCode = httpStatusCode
        };
    }

    public static ServiceResult Fail(List<string> errorMessages,HttpStatusCode httpStatusCode = HttpStatusCode.BadRequest)
    {
        return new ServiceResult
        {
            ErrorMessage = errorMessages,
            HttpStatusCode = httpStatusCode
        };
    }

    public static ServiceResult Fail(string errorMessage, HttpStatusCode httpStatusCode = HttpStatusCode.BadRequest)
    {
        return new ServiceResult
        {
            ErrorMessage = [errorMessage],
            HttpStatusCode = httpStatusCode
        };
    }
}
