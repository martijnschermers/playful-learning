using Core.Domain;

#pragma warning disable CS8618

namespace WebService.GraphQL;

public class GameNightPayload
{
    public GameNight GameNight { get; set; }
    public string Message { get; set; }
}