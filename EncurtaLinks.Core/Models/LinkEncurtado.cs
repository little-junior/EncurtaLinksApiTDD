namespace EncurtaLinks.Core.Models
{
    public class LinkEncurtado
    {
        //Todo: arrumar nome das classes e gerar outro banco
        public LinkEncurtado() { }

        public int Id { get; set; }
        public string UrlOriginal { get; set; }
        public string UrlGerado { get; set; }
        public string UltimaParteUrl { get; set; }
        public int TempoValidadeSegundos { get; set; }
        public DateTime DataCriacao { get; set; }
    }
}