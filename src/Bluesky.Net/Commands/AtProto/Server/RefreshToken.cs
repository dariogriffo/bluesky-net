namespace Bluesky.Net.Commands.AtProto.Server;

using Models;

public record RefreshToken(string AccessJwt, string RefreshJwt, string Handle, Did Did){ }
