using System.Collections.Generic;
using Models;

namespace service.Repositories.Interface
{
    public interface IUser
    {
         void AddUser(User user);
         void UpdateUser(User user);
         void Delete(int id);
         User UserLogin(string EmailID, string Password);
         IEnumerable<User> GetAllUsers();
         IEnumerable<User> GetAllUser();//For User Role Drop Down

    }
}