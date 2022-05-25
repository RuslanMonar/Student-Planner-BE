using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.AuthService.Dto
{
    public class TaskCompleteDto
    {
        public bool taskIsCompleted { get; set; }
        public int taskId { get; set; }
    }
}
