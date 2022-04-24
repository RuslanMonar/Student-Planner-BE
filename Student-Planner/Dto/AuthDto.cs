namespace Student_Planner.Dto
{
    public class AuthDto
    {
        public string Token { get; set; }

        public IEnumerable<string> Errors { get; set; }

    }
}
