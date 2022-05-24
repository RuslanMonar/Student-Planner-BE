namespace Domain
{
    public class TaskTrack
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public double TimeSpentMinutes { get; set; }

        public Tasks Tasks { get; set; }
    }
}
