using System;
using System.AddIn.Hosting;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AddInHostExternalProcess;
using HostView;
using NLog;
using Quartz;
using Quartz.Impl;

namespace AddInHost
{
    public class Program
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();
        private static List<AddInActivationInfo> addins = new List<AddInActivationInfo>(); 

        public static void Main(string[] args)
        {
            // create quartz scheduler
            ISchedulerFactory schedulerFactory = new StdSchedulerFactory();
            var scheduler = schedulerFactory.GetScheduler();
            
            string path = args.Length > 0 ? args[0] : Environment.CurrentDirectory;
            AddInStore.Update(path);

            var addInTokens = AddInStore.FindAddIns(typeof(ScheduledTaskHostView), path);

            foreach (var addInToken in addInTokens)
            {
                _logger.Debug("Found add-in: " + addInToken.AddInFullName);

                addins.Add(ActivationHelper.ActivateAddIn(addInToken));
            }

            foreach (var info in addins)
            {
                scheduler.ScheduleJob(info.JobDetail, info.Trigger);
            }

            scheduler.Start();
            Console.ReadLine();
        }

        private static AddInActivationInfo GetInfoByToken(AddInToken token)
        {
            return addins.FirstOrDefault(a => a.Token == token);
        }

        private static AddInActivationInfo GetInfoByProcess(AddInProcess process)
        {
            return addins.FirstOrDefault(a => a.Process == process);
        }

        private static AddInActivationInfo GetInfoByAddIn(ScheduledTaskHostView addin)
        {
            return addins.FirstOrDefault(a => a.AddIn == addin);
        }
    }

}
