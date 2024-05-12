namespace Unicam.Ristorante.Application.Options
{
    public class JwtAuthenticationOption
    {
        public string Key { get; set; } = string.Empty;
        public string Issuer { get; set; } = string.Empty;
        public int MinutesToExpiration { get; set; } = 0;
    }
}
