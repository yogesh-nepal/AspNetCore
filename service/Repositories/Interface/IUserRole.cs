using Models;

namespace service.Repositories.Interface
{
    public interface IUserRole
    {
        void AddUserRole(UserRoleModel uRole);
        void Delete(UserRoleModel uRole);
    }
}