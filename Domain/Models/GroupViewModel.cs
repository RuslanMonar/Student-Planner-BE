namespace Domain.Models
{
    public class GroupViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Color { get; set; }
        public List<ProjectViewModel> Projects { get; set; }
    }
}
