namespace Unicam.Ristorante.Application.Models.Responses
{
    public class CreateTokenResponse
    {
        public string Token { get; set; } = string.Empty;

        public CreateTokenResponse(string token)
        {
            Token = token;
        }
    }
}
