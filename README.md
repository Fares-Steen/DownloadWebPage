# DownloadWebPage
The application will start at the home page and download the html of it and then look at any <a> elements that belong to the same website and download them as well and repeat that in a recursive way.

Every single page will be handled concurrently.

The downloaded pages will be located beside the application executable files.

There is also a test function that will test the main download function using mocking.

To run the app just run it with "dotnet run" :) (you need to have .net core 3.1 installed)