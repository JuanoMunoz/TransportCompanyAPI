namespace TransportCompany.Helpers
{
    public class ApiErrorMessage
    {
        public string Message { get; set; }
        public string Tip { get; set; }
        public ApiErrorMessage(string message, string tip) { 
            Message = message;
            Tip = tip;
        }
    }
}
