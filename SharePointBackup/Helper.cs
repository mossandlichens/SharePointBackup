namespace SharePointBackup
{
    using NLog;
    using System.Net;

    class Helper
    {
        internal static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        internal static ICredentials GetNetworkCredential(string serviceAccountLogonName, string serviceAccountPassword)
        {
            return new NetworkCredential(serviceAccountLogonName, serviceAccountPassword);
        }
    }
}
