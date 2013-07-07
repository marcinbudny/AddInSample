using System;

namespace HostView
{
    [Serializable]
    public class ScheduleOptions
    {
        public string CronExpression { get; set; }
    }
}
