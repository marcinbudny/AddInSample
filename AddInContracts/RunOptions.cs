using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddInContracts
{
    [Serializable]
    public class RunOptions
    {
        public DateTime PointInTime { get; set; }
    }
}
