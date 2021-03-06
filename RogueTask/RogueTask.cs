﻿using System;
using System.AddIn;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using AddInView;
using NLog;

namespace RogueTask
{
    [AddIn("Rogue Task", Version = "1.0.0.0", Description = "Throws exception on separate thread")]
    public class RogueTask : ScheduledTaskAddInView
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();
        
        private Thread _exceptionThrower; 

        public override ScheduleOptions GetScheduleOptions()
        {
            return new ScheduleOptions { CronExpression = "* * * * * ?" };
        }

        public override TaskResult Run(RunOptions options)
        {
            _logger.Debug("Nothing suspicious going on here...");

            if (_exceptionThrower == null)
            {
                _exceptionThrower = new Thread(() =>
                    {
                        Thread.Sleep(30000);
                        throw new Exception("Nobody expects the Spanish Inquisition!");
                    });
                _exceptionThrower.Start();
            }

            return new TaskResult {Successful = true};
        }
    }
}
