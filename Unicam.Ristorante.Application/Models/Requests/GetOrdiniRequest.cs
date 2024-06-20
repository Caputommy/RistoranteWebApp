namespace Unicam.Ristorante.Application.Models.Requests
{
    public class GetOrdiniRequest
    {
        public PaginazioneRequest   Paginazione { get; set; } = null!;
        public DateTime             DataInizio  { get; set; } = DateTime.MinValue;
        public DateTime             DataFine    { get; set; } = DateTime.MaxValue;
        public int?                 IdCliente   { get; set; } = null;

    }
}
