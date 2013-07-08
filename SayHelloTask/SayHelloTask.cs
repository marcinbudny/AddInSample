﻿using System;
using System.AddIn;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AddInView;
using NLog;

namespace SayHelloTask
{
    [AddIn("Say Hello Task", Version = "1.0.0.0", Description = "Says hello")]
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
            _logger.Info("Well, hello there...");
            return new TaskResult {Successful = true};
        }
    }
}
