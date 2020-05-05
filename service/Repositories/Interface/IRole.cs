using System.Collections.Generic;
using Models;

namespace service.Repositories.Interface
{
    public interface IRole
    {
        IEnumerable<RoleModel> GetAllRole();
    }
}