using System.AddIn.Hosting;
using HostView;
using Quartz;

namespace AddInHost
{
    public class AddInActivationInfo
    {
        public AddInToken Token { get; set; }

        public ScheduledTaskHostView  AddIn { get; set; }

        public IJobDetail JobDetail { get; set; }

        public ITrigger Trigger { get; set; }
    }
}
