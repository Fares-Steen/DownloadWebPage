using System;
using System.Threading.Tasks;

namespace downloadWebPage.Services.HttpServices
{
	public interface IHttpService : IDisposable
	{
		Task<string> DownloadPage(string fullUrl);
	}
}