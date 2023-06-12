namespace MyBlazorCourse.Shared.Model;

public class Photo
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public byte[] Data { get; set; } = new byte[0];
    public string ImageMimetype { get; set;} = string.Empty;
}