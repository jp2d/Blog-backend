namespace Blog.Api.DTO
{
    public class PostDto
    {
        public int id { get; set; }
        public string Titule { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
