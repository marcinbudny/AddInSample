using System;
using System.AddIn.Pipeline;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AddInContracts;
using AddInView;

namespace AddInAdapter
{
    [AddInAdapter]
    public class ScheduledTaskToViewAdapter : ContractBase, IScheduledTask
    {
        private readonly ScheduledTaskAddInView _view;

        public ScheduledTaskToViewAdapter(ScheduledTaskAddInView view)
        {
            _view = view;
        }
        
        public AddInContracts.ScheduleOptions GetScheduleOptions()
        {
            var options = _view.GetScheduleOptions();
            return ReflectionCopier.Copy<AddInContracts.ScheduleOptions>(options);
        }

        public AddInContracts.TaskResult Run(AddInContracts.RunOptions options)
        {
            var viewOptions = ReflectionCopier.Copy<AddInView.RunOptions>(options);
            var result = _view.Run(viewOptions);
            return ReflectionCopier.Copy<AddInContracts.TaskResult>(result);
        }
    }
}
