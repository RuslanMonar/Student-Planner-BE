using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class TasksStatisticViewModel
    {
        public string ProjectTitle { get; set; }
        public string ProjectColor { get; set; }
        public string TaskTitle { get; set; }
        public List<TaskTrackViewModel> TasksTrack { get; set; }

    }
}
