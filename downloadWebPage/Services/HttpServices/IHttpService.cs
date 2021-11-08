using System.Threading.Tasks;

namespace downloadWebPage.Services.HttpServices
{
    public interface IHttpService 
	{
		Task<string> DownloadPage(string fullUrl);
	}
}