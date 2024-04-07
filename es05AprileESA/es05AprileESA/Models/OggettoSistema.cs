using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
namespace es05AprileESA.Models
{
    [Table("oggetto_sistema")]
    public class OggettoSistema
    {
        
        [Column("oggettoRIF")]
        public int OggettoIdRiferimento { get; set; }
        
      
        [Column("sistemaRIF")][JsonIgnore]        
        public int SistemaIdRiferimento { get; set; }

        
        public Oggetto OggettoRif { get; set; } = null!;

        [JsonIgnore]
        public Sistema SistemaRif { get; set; } = null!;
    }
}
