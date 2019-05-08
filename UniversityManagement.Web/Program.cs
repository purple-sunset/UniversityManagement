using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using UniversityManagement.Utilities;

namespace UniversityManagement.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                Logger.StartLogger();
                CreateWebHostBuilder(args).Build().Run();
                return;
            }
            catch (Exception)
            {
                return;
            }
            finally
            {
                Logger.EndLogger();
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
