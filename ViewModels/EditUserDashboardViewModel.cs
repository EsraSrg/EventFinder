public class EditUserDashboardViewModel
{
    public string Id { get; set; }
    public string? ProfileImageUrl { get; set; }
    public string? City { get; set; } // hard coded
    public string? State { get; set; }
    public IFormFile Image { get; set; }
}