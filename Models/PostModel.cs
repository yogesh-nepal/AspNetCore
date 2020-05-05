using System;
namespace Models
{
    public class PostModel
    {
        public int PostID { get; set; }
        public int CategoryID { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string FullDescription { get; set; }
        public DateTime PublishDate { get; set; } = DateTime.Now;
        public bool IsActive { get; set; }
        public string Remarks { get; set; }
        public string AuthorName { get; set; }
        public string Tag { get; set; }
        public string ImageURL { get; set; }
        public DateTime? UpdatedTime { get; set; } = DateTime.Now;
        public string UpdatedBy { get; set; }
    }
}