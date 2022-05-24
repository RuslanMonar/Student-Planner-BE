using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.AuthService.Dto
{
    public class TrackTimeDto
    {
        public int TaskId { get; set; }
        public DateTime Date { get; set; }
        public double? TimeSpentMinutes { get; set; }
    }
}
