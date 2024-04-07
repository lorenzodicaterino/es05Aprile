using es05AprileESA.DTO;
using es05AprileESA.Models;
using es05AprileESA.Repositories;


namespace es05AprileESA.Services
{
    public class OggettoService
    {
        private readonly OggettoRepository repo;

        public OggettoService(OggettoRepository repos)
        {
            repo = repos;
        }

        public bool InserisciOggetto(OggettoDTO dto)
        {
            Oggetto ogg = new Oggetto()
            {
                CodiceOggetto = Guid.NewGuid().ToString().ToUpper(),
                NomeOggetto = dto.NomO,
                DataScopertaOggetto = dto.DatO,
                ScopritoreOggetto = dto.ScoO,
                TipologiaOggetto = dto.TipO,
                DistanzaDallaTerraOggetto = dto.DdtO,
                ModuloOggetto = dto.ModO,
                AzimutOggetto = dto.AziO
            };

            return repo.Create(ogg);
        }

        public bool ModificaOggetto (OggettoDTO dto)
        {
            Oggetto ogg = new Oggetto()
            {
                CodiceOggetto = dto.CodO,
                NomeOggetto = dto.NomO,
                DataScopertaOggetto = dto.DatO,
                ScopritoreOggetto = dto.ScoO,
                TipologiaOggetto = dto.TipO,
                DistanzaDallaTerraOggetto = dto.DdtO,
                ModuloOggetto = dto.ModO,
                AzimutOggetto = dto.AziO
            };

            return repo.Update(ogg);
        }

        public bool EliminaOggetto(string codice)
        {
            if (codice is not null)
            {
                return repo.Delete(codice);
            }
            return false;
        }

        public List<OggettoDTO> GetAllOggetti()
        {
            List<OggettoDTO> dtos = repo.GetAll().Select(o => new OggettoDTO()
            {
                CodO=o.CodiceOggetto,
                NomO=o.NomeOggetto,
                DatO=o.DataScopertaOggetto,
                ScoO=o.ScopritoreOggetto,
                TipO=o.TipologiaOggetto,
                DdtO=o.DistanzaDallaTerraOggetto,
                ModO=o.ModuloOggetto,
                AziO=o.AzimutOggetto
            }).ToList();

            return dtos;
        } 

        public OggettoDTO GetByCodiceOggetto (string codice)
        {
            Oggetto o = repo.GetByCodice(codice);

            OggettoDTO dto = new OggettoDTO()
            {
                CodO = o.CodiceOggetto,
                NomO = o.NomeOggetto,
                DatO = o.DataScopertaOggetto,
                ScoO = o.ScopritoreOggetto,
                TipO = o.TipologiaOggetto,
                DdtO = o.DistanzaDallaTerraOggetto,
                ModO = o.ModuloOggetto,
                AziO = o.AzimutOggetto
            };

            return dto;
        }

        public List<SistemaDTO> RecuperaSistemiPerOggetto(string code)
        {
            List<SistemaDTO> s = repo.GetSistema(code).Select(o => new SistemaDTO
            {
                CodS= o.CodiceSistema,
                NomS = o.NomeSistema,
                TipS = o.TipoSistema
            }).ToList();

            return s;
        }

        public bool InserisciInSistema(string codiceO, string codiceS)
        {
            List<Sistema> x = repo.GetSistema(codiceO);
            List<Sistema> tutti = repo.RecuperaSistemi();

            bool ins = true;

            foreach (Sistema s in tutti)
            {
                if (s.CodiceSistema == codiceS)
                {
                    foreach(Sistema si in x)
                    {
                        if (s.TipoSistema == si.TipoSistema)
                            ins = false;
                    }
                }
            }
            
            if(ins)
                return repo.InserisciInSistema(codiceO, codiceS);
            else
                return false;
        }

    }
}
