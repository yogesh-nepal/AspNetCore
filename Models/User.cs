using System.Collections.Generic;

namespace Models
{
    public class User
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
        public string Role { get; set; }
        public int RoleID { get; set; }

        public IList<RoleModel> Roles { get; set; }
        //multiple role cha haina aba?
        //yo role vanne column xane ni user model databse ma

    }
}