using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddInContracts
{
    [Serializable]
    public class TaskResult
    {
        public bool Successful { get; set; }

        public Exception Exception { get; set; }
    }
}
