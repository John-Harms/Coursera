using System.Runtime.CompilerServices;
using System.Threading.Tasks;

public class Program
{
    async Task DownloadDataAsync()
    {
        try
        {
            throw new Exception("Whoops");
            await Task.Delay(500);
            Console.WriteLine("finished task");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error Here {ex.Message}");
        }
    }

    async Task DownloadDataAsync2()
    {
        await Task.Delay(900);
        Console.WriteLine("finished task2");
    }

    public static async Task Main()
    {
        Program program = new Program();
        await Task.WhenAll(program.DownloadDataAsync(), program.DownloadDataAsync2());
        Console.WriteLine("Finished Both Thingies");
    }
}