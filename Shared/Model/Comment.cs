namespace MyBlazorCourse.Shared.Model;

public class Comment
{
    public int Id { get; set; }
    public int PhotoId { get; set; }
    public string? Title { get; set; }
    public string? Content { get; set; }
    public DateTime SubmittedOn { get; set; }
}