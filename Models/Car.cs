namespace FormulaHQ.API.Models
{
    public class Car
    {
        public Guid CarID { get; set; }
        public string Model { get; set; }
        public Guid TeamID { get; set; }
        public Guid SeasonID { get; set; }
        public string TechnicalDetails { get; set; }
        public bool isDeleted { get; set; }

        public Team Team { get; set; }
        public Season Season { get; set; }

    }
    public class CarParamModel
    {
        public Guid CarID { get; set; }
    }

    public class CarResponseModel
    {
        public int MessageID { get; set; }
        public string Message { get; set; }
        public Car? Car { get; set; }
    }
    public class CarListResponseModel
    {
        public int MessageID { get; set; }
        public string Message { get; set; }
        public List<Car>? Cars { get; set; }
    }
}
