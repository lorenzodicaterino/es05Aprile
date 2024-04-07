using System.ComponentModel.DataAnnotations.Schema;

namespace es05AprileESA.Models
{
    [Table("sistema")]
    public class Sistema
    {
        [Column("sistemaID")]
        public int SistemaId { get; set; }

        [Column("codice_sistema")]
        public string CodiceSistema { get; set; } = Guid.NewGuid().ToString();

        [Column("nome")]
        public string NomeSistema { get; set; }

        [Column("tipo")]
        public string TipoSistema { get; set; }

        [Column("deleted")]
        public DateTime? DeletedSistema{ get; set; }
        public List<OggettoSistema>? OggettiSistema{ get; set; }
    }
}
