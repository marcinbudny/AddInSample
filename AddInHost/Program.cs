using System;
using System.AddIn.Hosting;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HostView;
using NLog;

namespace AddInHost
{
    class Program
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();
        
        static void Main(string[] args)
        {
            string path = Environment.CurrentDirectory;
            AddInStore.Update(path);

            var addInTokens = AddInStore.FindAddIns(typeof (ScheduledTaskHostView), path);
            foreach (var addInToken in addInTokens)
            {
                _logger.Debug("Found add-in: " + addInToken.AddInFullName);
            }

            var addins = addInTokens.Select(a => 
                a.Activate<ScheduledTaskHostView>(AddInSecurityLevel.FullTrust, a.AddInFullName)).ToList();

            while (true)
            {
                _logger.Debug("Running tasks");

                foreach (var task in addins)
                {
                    try
                    {
                        task.Run(new RunOptions {PointInTime = DateTime.Now});
                    }
                    catch (Exception ex)
                    {
                        _logger.ErrorException("Running task resulted in exception", ex);
                    }
                }
                
                Thread.Sleep(1000);
            }
        }
    }
}
