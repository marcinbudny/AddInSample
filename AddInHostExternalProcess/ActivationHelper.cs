using System;
using System.AddIn.Hosting;
using AddInHostExternalProcess;
using HostView;
using NLog;
using Quartz;
using Quartz.Impl;

namespace AddInHost
{
    static internal class ActivationHelper
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        public static AddInActivationInfo ActivateAddIn(AddInToken token)
        {
            var process = new AddInProcess();

            process.ShuttingDown +=
                (sender, eventArgs) =>
                _logger.Warn(String.Format("Proccess for AddIn {0} is shutting down!", token.AddInFullName));

            var addin = token.Activate<ScheduledTaskHostView>(process, AddInSecurityLevel.FullTrust);

            var options = addin.GetScheduleOptions();

            var activationInfo = new AddInActivationInfo
                {
                    AddIn = addin,
                    Process = process,
                    Token = token,
                    JobDetail = JobBuilder.Create<RunTaskJob>()
                                          .WithIdentity(token.AddInFullName)
                                          .Build(),
                    Trigger = TriggerBuilder.Create()
                                            .WithCronSchedule(options.CronExpression)
                                            .StartNow()
                                            .Build()
                };

            // pass reference to activation info to the job
            activationInfo.JobDetail.JobDataMap.Add("ActivationInfo", activationInfo);

            return activationInfo;
        }
    }
}