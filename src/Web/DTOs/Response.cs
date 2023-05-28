namespace Web.DTOs
{
    public class Response
    {
        public bool IsValid { get; set; }
        public string? Message { get; set; }
        public dynamic? Result { get; set; }
    }
}
