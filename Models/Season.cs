namespace FormulaHQ.API.Models
{
    public class Season
    {
        public Guid SeasonID { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
    }

    public class SeasonResponseModel
    {
        public int MessageID { get; set; }
        public string Message { get; set; }
        public Season? Season { get; set; }
    }
    public class SeasonListResponseModel
    {
        public int MessageID { get; set; }
        public string Message { get; set; }
        public List<Season>? Seasons { get; set; }
    }
    public class SeasonParamModel
    {
        public Guid SeasonID { get; set; }
    }
}
