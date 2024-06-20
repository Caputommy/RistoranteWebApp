namespace Unicam.Ristorante.Application.Models.Requests
{
    public class CreateTokenRequest
    {
        public string Email     { get; set; } = null!;
        public string Password  { get; set; } = null!;
    }
}
