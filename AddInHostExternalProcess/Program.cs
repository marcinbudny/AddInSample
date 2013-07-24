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

namespace AddInHost
{
    class Program
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();
        private static List<AddInActivationInfo> addins = new List<AddInActivationInfo>(); 

        static void Main(string[] args)
        {
            string path = Environment.CurrentDirectory;
            AddInStore.Update(path);

            var addInTokens = AddInStore.FindAddIns(typeof(ScheduledTaskHostView), path);

            foreach (var addInToken in addInTokens)
            {
                _logger.Debug("Found add-in: " + addInToken.AddInFullName);

                addins.Add(ActivateAddIn(addInToken));
            }

            while (true)
            {
                _logger.Debug("Running tasks");

                foreach (var info in addins)
                {
                    try
                    {
                        info.AddIn.Run(new RunOptions {PointInTime = DateTime.Now});
                    }
                    catch (RemotingException ex)
                    {
                        _logger.ErrorException(
                            string.Format(
                                "Exception occured when communicating with addin {0}, probably process crashed",
                                info.Token.AddInFullName), ex);

                        _logger.Debug("Attempting to restart addin process");

                        info.Process.Shutdown();

                        var reactivatedInfo = ActivateAddIn(info.Token);
                        // store new information in existing info
                        info.AddIn = reactivatedInfo.AddIn;
                        info.Process = reactivatedInfo.Process;
                    }
                    catch (Exception ex)
                    {
                        _logger.ErrorException("Running task resulted in exception", ex);
                    }
                }

                Thread.Sleep(1000);
            }
        }

        private static AddInActivationInfo ActivateAddIn(AddInToken token)
        {
            var process = new AddInProcess();

            process.ShuttingDown +=
                (sender, eventArgs) =>
                _logger.Warn(string.Format("Proccess for AddIn {0} is shutting down!",
                                           GetInfoByProcess(sender as AddInProcess).Token.AddInFullName));
            var addin = token.Activate<ScheduledTaskHostView>(process, AddInSecurityLevel.FullTrust);

            return new AddInActivationInfo
            {
                AddIn = addin,
                Process = process,
                Token = token
            };
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
