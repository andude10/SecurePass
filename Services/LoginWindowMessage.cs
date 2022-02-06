namespace SecurePass.Services
{
    public class LoginWindowMessage
    {
        public LoginWindowMessage(bool isSuccessful)
        {
            IsSuccessful = isSuccessful;
        }

        public bool IsSuccessful { get; set; }
    }
}