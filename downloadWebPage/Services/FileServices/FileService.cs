using System.IO;

namespace downloadWebPage.Services.FileServices
{
    public class FileService : IFileService
	{
		private readonly string localPath;
		public FileService()
		{
			localPath = Directory.GetCurrentDirectory();

		}
		private string CreateAndGetFolderForPage(string pagePath)
		{
			pagePath = pagePath == "" ? "tretton37" : "tretton37\\" + pagePath;
			var createdFolder = Directory.CreateDirectory(@$"{localPath}\{pagePath}");
			return createdFolder.FullName;
		}

		public void SavePageOnDisk(string pagePath, string html)
		{
			var folder = CreateAndGetFolderForPage(pagePath);
			File.WriteAllText(@$"{folder}\index.html", html);
		}


	}
}
