using System;
using System.Threading.Tasks;

namespace SQLIntegrationTool.Applications
{
    internal class InitFromApplication : Application
    {
        public override Task Run(CommandLineArguments obj)
        {
            if(obj == null || String.IsNullOrEmpty(obj.Server) || String.IsNullOrEmpty(obj.Database) ||
                String.IsNullOrEmpty(obj.User) || String.IsNullOrEmpty(obj.Password))
            {
                throw new ArgumentException("Arguments are not correct");
            }

            return Task.Run(() => {

                this._configuration.FromConnectionString = $"Data Source={obj.Server};Initial Catalog={obj.Database};User Id={obj.User};Password={obj.Password};";
            });
        }
    }
}