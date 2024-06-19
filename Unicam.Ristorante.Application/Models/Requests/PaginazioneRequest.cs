namespace Unicam.Ristorante.Application.Models.Requests
{
    public class PaginazioneRequest
    {
        public int DimensionePagina { get; set; } = 10;
        public int NumeroPagina { get; set; } = 0;
    }
}
