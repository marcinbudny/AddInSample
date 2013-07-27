using System;
using System.Runtime.Remoting;
using HostView;
using NLog;
using Quartz;

namespace AddInHost
{
    [DisallowConcurrentExecution]
    public class RunTaskJob : IJob
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();
        
        public void Execute(IJobExecutionContext context)
        {
            var info = context.MergedJobDataMap["ActivationInfo"] as AddInActivationInfo;
            
            try
            {
                info.AddIn.Run(new RunOptions { PointInTime = DateTime.Now });
            }
            catch (Exception ex)
            {
                _logger.ErrorException("Running task resulted in exception", ex);
            }
        }
    }
}
