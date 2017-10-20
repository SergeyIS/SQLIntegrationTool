using System;
using System.Threading.Tasks;

namespace SQLIntegrationTool.Applications
{
    internal class TryCopyApplication : Application
    {
        public override Task Run(CommandLineArguments obj)
        {
            if (obj == null)
                throw new ArgumentException("Arguments are not correct");

            return Task.Run(() => {
                for (int i = 0; i < 100; i++)
                {
                    System.Threading.Thread.Sleep(100);
                    _processState = $"copying data: {i}";
                }
            });
            
        }
    }
}