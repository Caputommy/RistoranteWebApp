namespace Unicam.Ristorante.Application.Models.Requests
{
    public class CreateTokenRequest
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
