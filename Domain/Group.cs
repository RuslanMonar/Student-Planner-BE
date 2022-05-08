using System.Text.Json.Serialization;

namespace Domain
{
    public class Group
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public string Title { get; set; }
        public string Color { get; set; }
        [JsonIgnore]
        public List<Project> Projects { get; set; }
    }
}
