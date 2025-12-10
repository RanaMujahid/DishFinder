namespace DishFinder.Application.DTOs.Common;

public class ApiError
{
    public string Message { get; set; } = string.Empty;
    public string? Detail { get; set; }
    public int StatusCode { get; set; }
}
