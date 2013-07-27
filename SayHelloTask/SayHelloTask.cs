using System;
using System.AddIn;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AddInView;
using MultiversionLib;
using NLog;

namespace SayHelloTask
{
    [AddIn("Say Hello Task", Version = "1.0.0.0", Description = "Says hello")]
    public class SayHelloTask : ScheduledTaskAddInView
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();
        
        private NameGenerator _nameGenerator = new NameGenerator();

        public override ScheduleOptions GetScheduleOptions()
        {
            return new ScheduleOptions { CronExpression = "* * * * * ?" };
        }

        public override TaskResult Run(RunOptions options)
        {
            _logger.Debug("NLog version is " + typeof(Logger).Assembly.GetName().Version);
            _logger.Info("Well, hello there... " + _nameGenerator.GetName());
            return new TaskResult {Successful = true};
        }
    }
}
