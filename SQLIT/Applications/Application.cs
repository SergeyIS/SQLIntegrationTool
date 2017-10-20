using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLIntegrationTool.Applications
{
    class Application
    {
        protected ConfigurationStrings _configuration;
        protected String _processState;
        public String ProcessState
        {
            get { return _processState ?? "not known"; }
            set { value = _processState; }
        }
        public Application()
        {
            _configuration = new ConfigurationStrings();
            _processState = String.Empty;
        }
        public virtual Task Run(CommandLineArguments obj)
        {
            return Task.Run(() => { return; }); 
        }
    }
}
