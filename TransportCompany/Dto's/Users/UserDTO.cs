namespace TransportCompany.Dto_s.Users
{
    public class UserDTO
    {
        public string Username { get; set; }
        public DateTime JoiningDate { get; set; }

        public DateTime BirthDate { get; set; }

        public int Salary { get; set; }
        public int? BranchId { get; set; }
    }
}
