using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;
using AddInHost;
using HostView;
using NLog;
using Quartz;

namespace AddInHostExternalProcess
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
            catch (RemotingException ex)
            {
                _logger.ErrorException(
                    string.Format(
                        "Exception occured when communicating with addin {0}, probably process crashed",
                        info.Token.AddInFullName), ex);

                _logger.Debug("Attempting to restart addin process");

                info.Process.Shutdown();

                var reactivatedInfo = ActivationHelper.ActivateAddIn(info.Token);
                // store new information in existing info
                info.AddIn = reactivatedInfo.AddIn;
                info.Process = reactivatedInfo.Process;
            }
            catch (Exception ex)
            {
                _logger.ErrorException("Running task resulted in exception", ex);
            }
        }
    }
}
