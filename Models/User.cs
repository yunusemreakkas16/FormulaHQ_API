namespace FormulaHQ.API.Models
{
    public class User
    {
        public Guid UserID { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }
        public string Password { get; set; }

        public string Role { get; set; }
        public DateTime RegistrationDate { get; set; }

        public bool isDeleted { get; set; }

    }

    public class UserResponseModel
    {
        public int MessageId { get; set; }
        public string MessageDescription { get; set; }
        public User? User { get; set; }
    }
    public class UserListResponseModel
    {
        public int MessageId { get; set; }
        public string MessageDescription { get; set; }
        public List<User>? Users { get; set; }
    }
    public class UserParamModel
    {
        public Guid UserID { get; set; }
    }
}
