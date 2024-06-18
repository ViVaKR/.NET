using System.Text.Json.Serialization;

namespace CampWebAPIClient;

public record class Repository(
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("description")] string Description,
    [property: JsonPropertyName("html_url")] Uri GitHubHomeUrl,
    [property: JsonPropertyName("homepage")] Uri Homepage,
    [property: JsonPropertyName("watchers")] int Watches,
    [property: JsonPropertyName("pushed_at")] DateTime LastPushUtc
    )
{
    public DateTime LastPush => LastPushUtc.ToLocalTime();
}
