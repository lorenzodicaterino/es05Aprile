using System.ComponentModel.DataAnnotations.Schema;

namespace es05AprileESA.Models
{
    [Table("oggetto")]
    public class Oggetto
    {
        [Column("oggettoID")]
        public int? OggettoId { get; set; }

        [Column("codice_oggetto")]
        public string? CodiceOggetto { get; set; }

        [Column("nome")]
        public string? NomeOggetto { get; set; }

        [Column("data_scoperta")]
        public DateOnly? DataScopertaOggetto { get; set; }

        [Column("scopritore")]
        public string? ScopritoreOggetto { get; set; }

        [Column("tipologia")]
        public string? TipologiaOggetto { get; set; }

        [Column("distanza_dalla_terra")]
        public decimal? DistanzaDallaTerraOggetto { get; set; }

        [Column("modulo")]
        public decimal? ModuloOggetto { get; set; }

        [Column("azimut")]
        public decimal? AzimutOggetto { get; set; }

        [Column("deleted")]
        public DateTime? DeletedOggetto { get; set; }

        public List<OggettoSistema>? OggettiSistema { get; set; }
    }
}
