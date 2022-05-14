namespace Domain
{
    public class Project
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public string Title { get; set; }
        public string Color { get; set; }
        public int? GroupdId { get; set; }

        public Group Group { get; set; }
        public List<Tasks> Tasks { get; set; }
    }
}
