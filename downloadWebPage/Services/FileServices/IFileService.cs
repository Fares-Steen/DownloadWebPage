namespace downloadWebPage.Services.FileServices
{
	public interface IFileService
	{
		void SavePageOnDisk(string pagePath, string html);
	}
}