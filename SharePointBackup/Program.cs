namespace SharePointBackup
{
    using System;
    using Fclp;
    using System.IO;

    class Program
    {
        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.UnhandledException += UnhandledExceptionHandler;

            var parser = new FluentCommandLineParser<AppArgs>();
            parser.Setup(arg => arg.SiteUrl).As('s', "siteUrl").SetDefault(null).Required();
            parser.Setup(arg => arg.ListNames).As('l', "listNames").SetDefault(null).Required();
            parser.Setup(arg => arg.UserName).As('u', "userName").SetDefault(null).Required();
            parser.Setup(arg => arg.Password).As('p', "password").SetDefault(null).Required();

            var result = parser.Parse(args);

            if (result.HasErrors == false && 
                string.IsNullOrEmpty(parser.Object.SiteUrl) == false &&
                string.IsNullOrEmpty(parser.Object.ListNames) == false &&
                string.IsNullOrEmpty(parser.Object.UserName) == false &&
                string.IsNullOrEmpty(parser.Object.Password) == false)
            {
                var lists = parser.Object.ListNames.Split(',');

                foreach (var list in lists)
                {
                    // Get custom list items
                    var listDataTable = SharePointHelper.GetAllListItems(parser.Object.SiteUrl, list, parser.Object.UserName, parser.Object.Password);

                    // Save custom list items as Excel
                    ExcelHelper.SaveList(listDataTable);
                }
                
            }
            else
            {
                Console.WriteLine(result.ErrorText);
            }
        }

        private static void UnhandledExceptionHandler(object sender, UnhandledExceptionEventArgs e)
        {
            var exception = e.ExceptionObject as Exception;
            if (exception != null)
            {
                File.WriteAllText("ErrorMessage.txt", exception.Message);
            }
        }
    }
}
