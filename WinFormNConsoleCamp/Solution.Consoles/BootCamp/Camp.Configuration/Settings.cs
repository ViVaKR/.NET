namespace CampConfiguration;

public class Settings
{
    public required string Server { get; set; }
    public required string Database { get; set; }
    public required EndPoint[] Endpoints { get; set; }
}

public class EndPoint
{
    public required string IPAddress { get; set; }
    public int Port { get; set; }
}
