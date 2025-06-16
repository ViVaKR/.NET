
using System.Net.Http.Headers;
using System.Text.Json;
using CampWebAPIClient;

using HttpClient client = new();
client.DefaultRequestHeaders.Accept.Clear();
client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repostory Reporter");
var repos = await ProcessRepositoiesAsync(client);

foreach (var repo in repos)
{
    Console.WriteLine($"Name: {repo.Name}");
    Console.WriteLine($"Homepage: {repo.Homepage}");
    Console.WriteLine($"GitHub: {repo.GitHubHomeUrl}");
    Console.WriteLine($"Description: {repo.Description}");
    Console.WriteLine($"Watchers: {repo.Watches:#,0}");
    Console.WriteLine($"{repo.LastPush}");
    Console.WriteLine();
}

static async Task<List<Repository>> ProcessRepositoiesAsync(HttpClient client)
{
    var uri = "https://api.github.com/orgs/dotnet/repos";

    await using Stream stream = await client.GetStreamAsync(uri);
    var repos = await JsonSerializer.DeserializeAsync<List<Repository>>(stream);

    return repos ?? [];
}
