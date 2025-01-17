public partial class EmailService
{
    private class TokenInfo
    {
        public string Email { get; set; }
        public DateTime Expiration { get; set; }
        public bool Used { get; set; }
    }
}
