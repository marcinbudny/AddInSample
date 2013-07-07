using System;
using System.AddIn.Contract;
using System.AddIn.Pipeline;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddInContracts
{
    [AddInContract]
    public interface IScheduledTask : IContract
    {
        ScheduleOptions GetScheduleOptions();

        TaskResult Run(RunOptions options);
    }
}
