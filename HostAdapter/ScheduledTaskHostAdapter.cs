using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.AddIn.Pipeline;
using AddInContracts;
using HostView;

namespace HostAdapter
{
    [HostAdapter]
    public class ScheduledTaskHostAdapter : ScheduledTaskHostView
    {
        private readonly IScheduledTask _scheduledTask;
        private readonly ContractHandle _contractHandle;

        public ScheduledTaskHostAdapter(IScheduledTask scheduledTask)
        {
            _scheduledTask = scheduledTask;
            _contractHandle = new ContractHandle(scheduledTask);
        }


        public override HostView.ScheduleOptions GetScheduleOptions()
        {
            var options = _scheduledTask.GetScheduleOptions();
            return ReflectionCopier.Copy<HostView.ScheduleOptions>(options);
        }

        public override HostView.TaskResult Run(HostView.RunOptions options)
        {
            var contractOptions = ReflectionCopier.Copy<AddInContracts.RunOptions>(options);
            var result = _scheduledTask.Run(contractOptions);
            return ReflectionCopier.Copy<HostView.TaskResult>(result);
        }
    }
}
