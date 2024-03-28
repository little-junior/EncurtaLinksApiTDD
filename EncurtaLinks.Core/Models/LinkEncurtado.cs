namespace EncurtaLinks.Core.Models
{
    public class LinkEncurtado
    {
        public LinkEncurtado() { }

        public Guid Id { get; set; }
        public string UrlOriginal { get; set; } = string.Empty;
        public string UrlGerado { get; set; } = string.Empty;
        public string UltimaParteUrl { get; set; } = string.Empty;
        public int TempoValidadeSegundos { get; set; }
        public DateTime DataCriacao { get; set; }
    }
}