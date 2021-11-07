using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace downloadWebPage.Services.HttpServices
{
    public class HttpService : IHttpService, IDisposable
	{
        private readonly HttpClient client;
		public HttpService()
		{
			client = new HttpClient();
		}

		public void Dispose()
		{
			client.Dispose();
		}

		public async Task<string> DownloadPage(string fullUrl)
		{
			Console.WriteLine($"Downloading from: {fullUrl}");
			var html = await client.GetStringAsync(fullUrl);
			return html;

		}
	}
}
