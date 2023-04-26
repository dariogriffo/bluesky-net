namespace Bluesky.Net.Commands;

using Models;

public record RefreshToken(string AccessJwt, string RefreshJwt, string Handle, Did Did){ }
