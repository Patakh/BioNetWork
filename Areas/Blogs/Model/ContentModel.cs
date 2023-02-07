namespace BioNetWork.Areas.Blogs.Model
{
    public class ContentModel 
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public byte[] Content { get; set; }
        public string Description { get; set; }
        public DateTime DayePost { get; set; }
        public string NumberLike { get; set; }
        public string Coment { get; set; }
    }
}
