using System;

namespace AddInView
{
    [Serializable]
    public class TaskResult
    {
        public bool Successful { get; set; }

        public Exception Exception { get; set; }
    }
}
