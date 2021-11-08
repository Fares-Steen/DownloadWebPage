using downloadWebPage.Services;
using downloadWebPage.Services.FileServices;
using downloadWebPage.Services.HttpServices;
using System.Threading.Tasks;

namespace downloadWebPage
{
    class Program
    {
		static async Task Main(string[] args)
		{
			DownloadService downloadService = new DownloadService("https://tretton37.com/", new HttpService(), new FileService());
			await downloadService.HandleUrl("");
		}
	}
}
