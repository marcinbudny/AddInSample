using System;
using System.AddIn.Pipeline;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddInView
{
    [AddInBase]
    public abstract class ScheduledTaskAddInView
    {
        public abstract ScheduleOptions GetScheduleOptions();

        public abstract TaskResult Run(RunOptions options);
    }
}
