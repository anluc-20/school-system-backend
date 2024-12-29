namespace SchoolSystem.Models.Response
{
    public class WebResponse
    {
        public int Success { get; set; }
        public string? Message { get; set; }
        public object? Data { get; set; }

        public WebResponse()
        {
            Success = 0;
        }
    }
}
