using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddInContracts
{
    [Serializable]
    public class ScheduleOptions
    {
        public string CronExpression { get; set; }
    }
}
