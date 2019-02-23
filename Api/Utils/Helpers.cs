using System;
using System.Data.SqlClient;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Api.Utils
{
    public class Helpers
    {

        public static string GetServerFolder()
        {
            string serverFolder;
            string exeFolder = "";
#if (DEBUG)
            exeFolder = "Debug";
#elif (PRODUCTION)
            exeFolder = "Production";
#elif (RELEASE)
            exeFolder = "Release";
#else
#endif

            serverFolder = Directory.GetParent(Directory.GetCurrentDirectory()).ToString() + Path.DirectorySeparatorChar + "Server" + Path.DirectorySeparatorChar + "bin" + Path.DirectorySeparatorChar + exeFolder + Path.DirectorySeparatorChar + "netcoreapp2.0" + Path.DirectorySeparatorChar;
            if (!Directory.Exists(serverFolder))
            {
                serverFolder = Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar;
            }

            return serverFolder;
        }

        public static string GetDbLocation(string appSettingsDbLocation)
        {
            return GetServerFolder() + appSettingsDbLocation + Path.DirectorySeparatorChar;
        }
    }
}
