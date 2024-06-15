namespace Unicam.Ristorante.Application.Models.Requests
{
    public class GetOrdiniRequest
    {
        public DateTime DataInizio { get; set; } = DateTime.MinValue;
        public DateTime DataFine { get; set; } = DateTime.MaxValue;
        public int? IdCliente { get; set; } = null!;
        public int DimensionePagina { get; set; } = 10;
        public int NumeroPagina { get; set; } = 0;

    }
}
