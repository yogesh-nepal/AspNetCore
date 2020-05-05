using System.Collections.Generic;
using Models;

namespace service.Repositories.Interface
{
    public interface IPost
    {
        void AddPost(PostModel post);
        void UpdatePost(PostModel post);
        void DeletePost(int id);
        PostModel GetPostByID(int id);
        IEnumerable<PostModel> ShowAllPost();
    }
}