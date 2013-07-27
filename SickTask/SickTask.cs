using System;
using System.AddIn;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AddInView;
using MultiversionLib;
using NLog;

namespace SickTask
{
    [AddIn("Sick Task", Version = "1.0.0.0", Description = "Is sick and throws exception")]
    public class SickTask : ScheduledTaskAddInView
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();
        private static string[] _thingsToSay = { ", I feel sick...", ", seriously, I think I'm gonna faint!", ", blaaargh..." };

        private NameGenerator _nameGenerator = new NameGenerator();
        private int _state = 0;

        public override ScheduleOptions GetScheduleOptions()
        {
            return new ScheduleOptions { CronExpression = "* * * * * ?" };
        }

        public override TaskResult Run(RunOptions options)
        {
            _logger.Debug("NLog version is " + typeof(Logger).Assembly.GetName().Version);
            
            _logger.Info(_nameGenerator.GetName() + _thingsToSay[_state++ % _thingsToSay.Length]);
            if(_state % _thingsToSay.Length == 0)
                throw new Exception("(falls to the ground)");

            return new TaskResult { Successful = true };
        }
    }
}
