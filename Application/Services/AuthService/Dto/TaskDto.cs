namespace Application.Services.AuthService.Dto
{
    public class TaskDto
    {
        public string Title { get; set; }
        public int TomatoCount { get; set; }
        public int TomatoLength { get; set; }
        public int TotalTime { get; set; }
        public string Flag { get; set; }
        public DateTime Date { get; set; }
        public string? Description { get; set; }
        public int ProjectId { get; set; }

    }
}
