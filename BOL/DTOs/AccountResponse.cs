namespace BOL.DTOs
{
    public class AccountResponse
    {
        public string Id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public string? img_url { get; set; }
        public string? img_url_id { get; set; }
    }
}
