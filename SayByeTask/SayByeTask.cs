using System;
using System.AddIn;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AddInView;
using NLog;

namespace SayByeTask
{
    [AddIn("Say Bye Task", Version = "1.0.0.0", Description = "Says bye")]
    public class SayHelloTask : ScheduledTaskAddInView
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        public override ScheduleOptions GetScheduleOptions()
        {
            return new ScheduleOptions();
        }

        public override TaskResult Run(RunOptions options)
        {
            _logger.Debug("NLog version is " + typeof(Logger).Assembly.GetName().Version);
            _logger.Info("See ya!");
            return new TaskResult { Successful = true };
        }
    }
}
