using System.Collections.Generic;
using Models;

namespace service.Repositories.Interface
{
    public interface IMenu
    {
         void AddMenu(MenuModel menu);
         void DeleteMenu(int id);
         IEnumerable<MenuModel> ShowMenu();
    }
}