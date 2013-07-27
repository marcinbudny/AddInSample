using System;
using System.AddIn.Hosting;
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
            var addin = token.Activate<ScheduledTaskHostView>(AddInSecurityLevel.FullTrust);

            var options = addin.GetScheduleOptions();

            var activationInfo = new AddInActivationInfo
                {
                    AddIn = addin,
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