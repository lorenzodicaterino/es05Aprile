using es05AprileESA.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text.Json.Serialization;

namespace es05AprileESA.Repositories
{
    public class OggettoRepository : IRepository<Oggetto>
    {

        private readonly ESAContext ctx;

        public OggettoRepository(ESAContext context)
        {
            ctx = context;
        }

        public bool Create(Oggetto entity)
        {
            bool res = false;
            try
            {
                ctx.Oggettos.Add(entity);
                ctx.SaveChanges();
                res= true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return res;
        }

        public bool Delete(string codice)
        {
            bool res = false;
            try
            {
                Oggetto? temp = ctx.Oggettos.FirstOrDefault(o => o.CodiceOggetto == codice);
                temp.DeletedOggetto = DateTime.Now;

                ctx.Entry(temp).Property(p => p.DeletedOggetto).IsModified = true;
                ctx.SaveChanges();
                res = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return res;
        }

        public Oggetto? Get(int id)
        {
            return ctx.Oggettos.FirstOrDefault(o => o.OggettoId == id && o.DeletedOggetto == null);
        }

        public IEnumerable<Oggetto> GetAll()
        {
            return ctx.Oggettos.Where(o => o.DeletedOggetto == null).ToList();
        }

        public Oggetto? GetByCodice(string codice)
        {

            return ctx.Oggettos.FirstOrDefault(o => o.CodiceOggetto == codice && o.DeletedOggetto == null);
        }

        public bool Update(Oggetto entity)
        {
            bool res = false;
            try
            {
                Oggetto temp = ctx.Oggettos.FirstOrDefault(o => o.CodiceOggetto == entity.CodiceOggetto);

                if (temp is not null)
                {
                    entity.OggettoId = temp.OggettoId;
                    entity.NomeOggetto = entity.NomeOggetto is not null ? entity.NomeOggetto : temp.NomeOggetto;
                    entity.DataScopertaOggetto = entity.DataScopertaOggetto != null ? entity.DataScopertaOggetto : temp.DataScopertaOggetto;
                    entity.ScopritoreOggetto = entity.ScopritoreOggetto is not null ? entity.ScopritoreOggetto : temp.ScopritoreOggetto;
                    entity.TipologiaOggetto = entity.TipologiaOggetto is not null ? entity.TipologiaOggetto : temp.TipologiaOggetto;
                    entity.DistanzaDallaTerraOggetto = entity.DistanzaDallaTerraOggetto > 0 ? entity.DistanzaDallaTerraOggetto : temp.DistanzaDallaTerraOggetto;
                    entity.ModuloOggetto = entity.ModuloOggetto > 0 ? entity.ModuloOggetto : temp.ModuloOggetto;
                    entity.AzimutOggetto = entity.AzimutOggetto > 0 ? entity.AzimutOggetto : temp.AzimutOggetto;
                    entity.DeletedOggetto = temp.DeletedOggetto;

                    ctx.Entry(temp).CurrentValues.SetValues(entity);
                    ctx.SaveChanges();
                    res = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return res;
        }

        public List<Sistema> GetSistema (string codice)
        {
            List<Sistema> sistemi = new List<Sistema>();

            if(codice is not null)
            {
                Oggetto sistemiPerOggetto = (Oggetto)ctx.Oggettos.Include(p => p.OggettiSistema).FirstOrDefault(o=>o.CodiceOggetto== codice);
                

                foreach(OggettoSistema o in sistemiPerOggetto.OggettiSistema)
                {
                    Sistema x = ctx.Sistemas.Find(o.SistemaIdRiferimento);
                    sistemi.Add(x);
                }
            }

            return sistemi;
        }

        public bool InserisciInSistema(string codiceO, string codiceS)
        {
            bool res = false;
            try
            {
                Oggetto o = ctx.Oggettos.FirstOrDefault(o => o.CodiceOggetto == codiceO);
                Sistema s = ctx.Sistemas.FirstOrDefault(o => o.CodiceSistema == codiceS);
                
                ctx.OggettoSistemas.Add(new OggettoSistema()
                    {
                        OggettoIdRiferimento= (int)o.OggettoId,
                        SistemaIdRiferimento= (int)s.SistemaId,
                        OggettoRif=o,
                        SistemaRif=s
                    });
                ctx.SaveChanges();

                res = true;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return res;
        }

        public List<Sistema> RecuperaSistemi()
        {
            return ctx.Sistemas.ToList();
        }
    }
}
