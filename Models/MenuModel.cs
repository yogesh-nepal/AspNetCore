namespace Models
{
    public class MenuModel
    {
        public int MenuID { get; set; }
        public string Menu { get; set; }
        public bool IsOpen { get; set; }
        public string MenuURL { get; set; }
        public string Under { get; set; }
        public bool IsActive { get; set; }
    }
}