public class Reader
{
    public int Id { get; set; }
    public string FullName { get; set; } = "";
    public string Email { get; set; } = "";
    public string PasswordHash { get; set; } = "";
    public DateTime RegistrationDate { get; set; } = DateTime.UtcNow;
    public int RenewalCount { get; set; }
}
