using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharePointBackup
{
    using System.Net;

    class Helper
    {
        internal static ICredentials GetNetworkCredential(string serviceAccountLogonName, string serviceAccountPassword)
        {
            return new NetworkCredential(serviceAccountLogonName, serviceAccountPassword);
        }
    }
}
