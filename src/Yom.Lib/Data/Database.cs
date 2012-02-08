using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yom.Lib.Data.EF;
using System.Configuration;
using System.Data.EntityClient;
using System.Diagnostics;

namespace Yom.Lib.Data
{
    public static class Database
    {
        public static void Create()
        {
            using (YomContainer container = new YomContainer())
            {
                container.Database.Create();
            }

            string connectionstring = ConfigurationManager.ConnectionStrings["YomContainer"].ConnectionString;

            EntityConnectionStringBuilder builder = new EntityConnectionStringBuilder(connectionstring);

            string providerconnectionstring = builder.ProviderConnectionString;


            Process process = new Process();
            process.StartInfo.FileName = @"C:\Windows\Microsoft.NET\Framework64\v4.0.30319\aspnet_regsql.exe";
            process.StartInfo.Arguments = string.Format("-A mr  -C \"{0}\"", providerconnectionstring);
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;

            process.Start();

            Console.WriteLine(process.StandardOutput.ReadToEnd());


            Console.ReadKey();
        }

        public static void Delete()
        {
            using (YomContainer container = new YomContainer())
            {
                container.Database.Delete();
            }
        }
    }
}
