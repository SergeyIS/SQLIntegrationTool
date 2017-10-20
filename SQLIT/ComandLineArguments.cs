using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fclp;

namespace SQLIntegrationTool
{
    class CommandLineArguments
    {
        public String Provider { get; set; }
        public String Server { get; set; }
        public String Database { get; set; }
        public String User { get; set; }
        public String Password { get; set; }

        public static void BuildParser(FluentCommandLineParser<CommandLineArguments> p)
        {
            p.Setup(arg => arg.Provider).As("provider").SetDefault("System.Data.SqlClient");
            p.Setup(arg => arg.Server).As('s', "server").Required();
            p.Setup(arg => arg.Database).As('d', "database").Required();
            p.Setup(arg => arg.User).As('u', "user").Required();
            p.Setup(arg => arg.Password).As('p', "password").Required();
        }
    }
}
