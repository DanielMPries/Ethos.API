public record GetUserResponse {
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime LastLogin { get; set; }
    public string? UserName { get; set; }
}