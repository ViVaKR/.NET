using Microsoft.Extensions.Hosting;

namespace Camp.BinReadWriter;

public class Worker(IBinWriter binWriter, IBinReader binReader) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        binWriter.Write(1, "hello.dat", 4.3, 87, true);
        await Task.Run(() => binWriter.Write(1, "hello.dat", 4.3, 87, true), stoppingToken).ContinueWith(x =>
        {

            if (x.IsCompleted)
                binReader.Read();
        }, stoppingToken);
    }
}
