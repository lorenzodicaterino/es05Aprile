using es05AprileESA.DTO;
using es05AprileESA.Models;
using es05AprileESA.Repositories;

namespace es05AprileESA.Services
{
    public class SistemaService
    {
        private readonly SistemaRepository repo;

        public SistemaService (SistemaRepository repos)
        {
            repo = repos;
        } 

        public bool InserisciSistema (SistemaDTO dto)
        {
            Sistema sistema = new Sistema()
            {
                CodiceSistema = Guid.NewGuid().ToString().ToUpper(),
                NomeSistema = dto.NomS,
                TipoSistema = dto.TipS

            };

            return repo.Create(sistema);
        }

        public bool ModificaSistema (SistemaDTO dto)
        {
            if(repo.GetByCodice(dto.CodS) is not null)
            {
                Sistema sistema = new Sistema()
                {
                    CodiceSistema = dto.CodS,
                    NomeSistema = dto.NomS,
                    TipoSistema  = dto.TipS
                
                };

                return repo.Update(sistema);
            }

            return false;
        }

        public bool EliminaSistema (string codice)
        {
            if(codice is not null)
            {
                return repo.Delete(codice);
            }
            return false;
        }

        public List<SistemaDTO> GetAllSistema()
        {
            List<SistemaDTO> sistemi = repo.GetAll().Select(p => new SistemaDTO()
            {
                NomS = p.NomeSistema,
                CodS=p.CodiceSistema,
                TipS=p.TipoSistema
            }).ToList();
           
            return sistemi;
        }

        public SistemaDTO GetByCodice (string codice)
        {
            Sistema temp = repo.GetByCodice(codice);
            SistemaDTO dto = new SistemaDTO()
            {
                NomS = temp.NomeSistema,
                CodS = temp.CodiceSistema,
                TipS = temp.TipoSistema,
            };

            return dto;
        }

        public List<OggettoDTO> RecuperaOggettiPerSistema(string code)
        {
            List<OggettoDTO> o = repo.GetOggetti(code).Select(o => new OggettoDTO
            {
                CodO = o.CodiceOggetto,
                NomO = o.NomeOggetto,
                DatO = o.DataScopertaOggetto,
                ScoO = o.ScopritoreOggetto,
                TipO = o.TipologiaOggetto,
                DdtO = o.DistanzaDallaTerraOggetto,
                ModO = o.ModuloOggetto,
                AziO = o.AzimutOggetto
            }).ToList();
            
            return o;
        }
    }
}
