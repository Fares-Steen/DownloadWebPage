using downloadWebPage.Services;
using downloadWebPage.Services.FileServices;
using downloadWebPage.Services.HttpServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DownloadWebPageTests
{
	[TestClass]
	public class DownloadServiceTests
	{

		[TestMethod]
		public async Task HandleUrlTest()
		{
			//Arrange
			string baseUrl = "www.test.com/";

			Dictionary<string, string> htmlPages = new Dictionary<string, string>();

			htmlPages.Add(baseUrl + "", @"<a href= ""/contact"">    <a href= ""/jobs"">");

			htmlPages.Add(baseUrl + "contact", @"<div hello>");
			htmlPages.Add(baseUrl + "jobs", @"<a href= ""/jobs/lund""> ");

			htmlPages.Add(baseUrl + "jobs" + "/lund", @"<a href= ""/jobs/lund/softwaredeveloper""> ");
			htmlPages.Add(baseUrl + "jobs" + "/lund" + "/softwaredeveloper", @"<h1 im a new job");

			Mock<IFileService> fileServiceMock;
			Mock<IHttpService> httpServiceMock;
			DownloadService downloadService;

			httpServiceMock = new Mock<IHttpService>();
			httpServiceMock.Setup(h => h.DownloadPage(It.IsAny<string>())).ReturnsAsync((string key) => { return GetValueFromDictionary(key, htmlPages); });

			fileServiceMock = new Mock<IFileService>();
			downloadService = new DownloadService(baseUrl, httpServiceMock.Object, fileServiceMock.Object);

			//Act
			await downloadService.HandleUrl("");

			//Assert
			fileServiceMock.Verify(f => f.SavePageOnDisk(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(htmlPages.Count));

			foreach (var item in htmlPages)
			{
				var path = item.Key.Replace(baseUrl, "");
				fileServiceMock.Verify(f => f.SavePageOnDisk(It.Is<string>(pagePath => pagePath == path), It.Is<string>(html => html == GetValueFromDictionary(item.Key, htmlPages))), Times.Once);

			}
		}

		private string GetValueFromDictionary(string key, Dictionary<string, string> dictionary)
		{
            dictionary.TryGetValue(key, out string value);
            return value;
		}
	}
}
