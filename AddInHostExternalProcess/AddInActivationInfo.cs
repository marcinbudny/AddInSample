using System;
using System.AddIn.Hosting;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HostView;

namespace AddInHostExternalProcess
{
    public class AddInActivationInfo
    {
        public AddInToken Token { get; set; }

        public AddInProcess Process { get; set; }

        public ScheduledTaskHostView  AddIn { get; set; }
    }
}
