using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.AuthService.Dto
{
    public class GetTasksByIdDto
    {
        public int TaskId { get; set; }
        public bool isCompleted { get; set; }
    }
}
