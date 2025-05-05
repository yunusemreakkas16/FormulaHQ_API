namespace FormulaHQ.API.Models
{
    public class Sponsor
    {
        public Guid SponsorID { get; set; }
        public string Name { get; set; }
        public Guid TeamID { get; set; }
        public double ContributionAmount { get; set; }
        public bool isDeleted { get; set; }

        //Navagation properties
        public Team Team { get; set; }
    }
    public class SponsorParamModel
    {
        public Guid SponsorID { get; set; }
    }
    public class SponsorResponseModel
    {
        public int MessageID { get; set; }
        public string Message { get; set; }
        public Sponsor? Sponsor { get; set; }
    }
    public class SponsorListResponseModel
    {
        public int MessageID { get; set; }
        public string Message { get; set; }
        public List<Sponsor>? Sponsors { get; set; }
    }


}
