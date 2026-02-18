namespace Blog.Api.DTO
{
    public class CreatePostDto
    {
        public string Titule { get; set; }
        public string Content { get; set; }
        public int UserId { get; set; }
    }
}
