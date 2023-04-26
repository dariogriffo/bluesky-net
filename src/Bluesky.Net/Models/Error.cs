namespace Bluesky.Net.Models;

public record Error(int StatusCode, ErrorDetail? Detail = default)
{
}