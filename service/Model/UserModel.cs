namespace service.Model
{
    public class UserModel
    {
        public int UserID { get; set; }
        public string UserName { get; set; }//FirstName+LastName
        public string Address { get; set; }
        public string Phone { get; set; }
        public string EmailID { get; set; }
        public string Gender { get; set; }
        public bool IsActive { get; set; }
        public string Remarks { get; set; }
        public string Password { get; set; }
    }
}