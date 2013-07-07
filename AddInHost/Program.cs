using System;
using System.AddIn.Hosting;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HostView;

namespace AddInHost
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = Environment.CurrentDirectory;
            AddInStore.Update(path);

            var addInTokens = AddInStore.FindAddIns(typeof (ScheduledTaskHostView), path);
            foreach (var addInToken in addInTokens)
            {
                Trace.TraceInformation(addInToken.AddInFullName);
            }

            var addins = addInTokens.Select(a => a.Activate<ScheduledTaskHostView>(AddInSecurityLevel.FullTrust)).ToList();

            while (true)
            {
                foreach (var task in addins)
                {
                    task.Run(new RunOptions {PointInTime = DateTime.Now});
                }
                
                //if(Console.KeyAvailable)
                //    break;
                Thread.Sleep(1000);
            }
        }
    }
}
