using System;
using Models;

namespace service.Repositories.Interface
{
    public interface IUserMenu
    {
        void AddUserMenu(UserMenuModel uMenu);
        void DeleteUserMenu(UserMenuModel uMenu);
    }
}