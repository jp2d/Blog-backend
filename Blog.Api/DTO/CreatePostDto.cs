namespace Blog.Api.DTO
{
    public class CreatePostDto
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public int UserId { get; set; }
    }
}
