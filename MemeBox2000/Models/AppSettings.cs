using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Socha3.MemeBox2000.Models
{
    public class AppSettings
    {
        public Logging Logging { get; set; }
        public bool ShowErrors { get; set; }
        public string AppName { get; set; }
        public string ConnectionStringName { get; set; }
        public string AllowedHosts { get; set; }
    }

    public class Logging
    {
        public LogLevel LogLevel { get; set; }
    }

    public class LogLevel
    {
        public string Default { get; set; }
    }
}
