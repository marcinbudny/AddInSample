using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AddInContracts;

namespace HostView
{
    public abstract class ScheduledTaskHostView
    {
        public abstract ScheduleOptions GetScheduleOptions();

        public abstract TaskResult Run(RunOptions options);
    }
}
