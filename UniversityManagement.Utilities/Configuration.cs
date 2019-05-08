using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace UniversityManagement.Utilities
{
    public static class Configuration
    {
        private static IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

        private static IConfigurationRoot configuration = builder.Build();

        public static IConfigurationSection MailSender { get => configuration.GetSection("MailSender"); }

        public static string MailServer { get => MailSender["MailServer"]; }
        public static string MailPort { get => MailSender["MailPort"]; }
        public static string MailAccount { get => MailSender["MailAccount"]; }
        public static string MailPassword { get => MailSender["MailPassword"]; }
    }
}
