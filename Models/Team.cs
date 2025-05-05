namespace FormulaHQ.API.Models
{
    public class Team
    {
        public Guid TeamID { get; set; }
        public string TeamName { get; set; }
        public int EstablishmentYear { get; set; }
        public string Country { get; set; }
        public bool isDeleted { get; set; }
    }

    public class TeamResponseModel
    {
        public int MessageId { get; set; }
        public string Message { get; set; }
        public Team? Team { get; set; }
    }
    public class TeamListResponseModel
    {
        public int MessageId { get; set; }
        public string Message { get; set; }
        public List<Team>? Teams { get; set; }
    }

    public class TeamDetailParamModel
    {
        public Guid TeamID { get; set; }
    }
}
