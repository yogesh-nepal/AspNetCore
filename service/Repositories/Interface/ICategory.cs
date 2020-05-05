using System;
using System.Collections.Generic;
using Models;

namespace service.Repositories.Interface
{
    public interface ICategory
    {
        void AddCategory(CategoryModel catModel);
        void DeleteCategory(int id);
        IEnumerable<CategoryModel> ShowCategory();
        
    }
}