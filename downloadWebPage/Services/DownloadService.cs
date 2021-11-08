using downloadWebPage.Services.FileServices;
using downloadWebPage.Services.HttpServices;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace downloadWebPage.Services
{
    public class DownloadService 
	{
        private readonly List<string> subPaths = new List<string>();
		private readonly object _lockObject = new object();
		private readonly IFileService fileService;
		private readonly IHttpService httpService;
		private readonly string BaseUrl;
		public DownloadService(string baseUrl, IHttpService httpService, IFileService fileService)
		{
			this.httpService = httpService;
			this.fileService = fileService;
			this.BaseUrl = baseUrl;

		}

		public async Task HandleUrl(string path)
		{
			var pageName = GetPageName(path);
			if (!IsPathAlreadyAdded(pageName))
			{
				AddToPathsList(pageName);
				var fullUrl = BaseUrl + path;
				var html = await httpService.DownloadPage(fullUrl);
				fileService.SavePageOnDisk(path, html);
				var allPathInPage = GetAllUrlsInPage(html);
				ConcurrentBag<Task> handleTasks = new ConcurrentBag<Task>();

				foreach (var pagePath in allPathInPage)
				{
					handleTasks.Add(HandleUrl(pagePath));
				}
				await Task.WhenAll(handleTasks);
			}
		}

		private string GetPageName(string url)
		{
			string[] paths = url.Split('/');

			return paths[^1];
		}

		private List<string> GetAllUrlsInPage(string htmlPage)
		{
			List<string> pageSubPaths = new List<string>();
			string hrefPattern = "<a\\s*href\\s*=\\s*\"/(?<url>.*?)\""; //match <a href = "/subpath/subpath" >

			try
			{
				Match regexMatch = Regex.Match(htmlPage, hrefPattern,
											   RegexOptions.IgnoreCase | RegexOptions.Compiled,
											   TimeSpan.FromSeconds(2));
				while (regexMatch.Success)
				{
					pageSubPaths.Add(regexMatch.Groups[1].ToString());
					regexMatch = regexMatch.NextMatch();
				}
			}
			catch (RegexMatchTimeoutException)
			{
				Console.WriteLine("The matching operation timed out.");
			}

			return pageSubPaths;
		}

		private void AddToPathsList(string path)
		{
			lock (_lockObject)
			{
				subPaths.Add(path);
			}
		}

		private bool IsPathAlreadyAdded(string path)
		{
			lock (_lockObject)
			{
				return subPaths.Contains(path);
			}
		}


	}
}
