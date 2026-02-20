namespace Blog.Api.DTO
{
    public class PostDto
    {
        public int id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
