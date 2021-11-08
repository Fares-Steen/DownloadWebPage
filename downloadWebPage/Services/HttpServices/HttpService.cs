using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace downloadWebPage.Services.HttpServices
{
    public class HttpService : IHttpService
	{
        private readonly HttpClient client;
		public HttpService()
		{
			client = new HttpClient();
		}

		public async Task<string> DownloadPage(string fullUrl)
		{
			Console.WriteLine($"Downloading from: {fullUrl}");
			var html = await client.GetStringAsync(fullUrl);
			return html;

		}
	}
}
