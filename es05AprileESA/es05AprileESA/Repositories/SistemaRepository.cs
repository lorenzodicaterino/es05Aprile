using es05AprileESA.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace es05AprileESA.Repositories
{
    public class SistemaRepository : IRepository<Sistema>
    {
        private readonly ESAContext ctx;

        public SistemaRepository(ESAContext context)
        {
            ctx = context;
        }

        public bool Create(Sistema entity)
        {
            bool res = false;
            try
            {
                ctx.Sistemas.Add(entity);
                ctx.SaveChanges();
                res = true;
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
                Sistema? temp = ctx.Sistemas.FirstOrDefault(s => s.CodiceSistema == codice);
                temp.DeletedSistema = DateTime.Now;

                ctx.Entry(temp).Property(p => p.DeletedSistema).IsModified = true;
                ctx.SaveChanges();

                //ctx.Sistemas.Update(temp);
                //ctx.SaveChanges();

                res = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return res;
        }

        public Sistema? Get(int id)
        {
            return ctx.Sistemas.FirstOrDefault(s=>s.SistemaId==id && s.DeletedSistema == null);
        }

        public IEnumerable<Sistema> GetAll()
        {
            return ctx.Sistemas.Where(s=>s.DeletedSistema==null).ToList();
        }

        public Sistema? GetByCodice(string codice)
        {
            return ctx.Sistemas.FirstOrDefault(s => s.CodiceSistema == codice && s.DeletedSistema == null);
        }

        public bool Update(Sistema entity)
        {
            bool res = false;
            try
            {
                //Sistema temp = ctx.Sistemas.this.GetByCodice(entity.CodiceSistema);
                Sistema temp = ctx.Sistemas.FirstOrDefault(t => t.CodiceSistema == entity.CodiceSistema);
                
                if(temp is not null)
                {
                    entity.SistemaId=temp.SistemaId;
                    entity.CodiceSistema=temp.CodiceSistema;
                    entity.NomeSistema = entity.NomeSistema is not null ? entity.NomeSistema : temp.NomeSistema;
                    entity.TipoSistema = entity.TipoSistema is not null ? entity.TipoSistema : temp.TipoSistema;

                    ctx.Entry(temp).CurrentValues.SetValues(entity);
                    ctx.SaveChanges();

                    res = true;
                }
                //ctx.Sistemas.Update(entity);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return res;
        }

        public List<Oggetto> GetOggetti(string codice)
        {
            List<Oggetto> oggetti= new List<Oggetto>();

            if (codice is not null)
            {
                Sistema oggettiPerSistema = ctx.Sistemas.Include(s => s.OggettiSistema).FirstOrDefault(s => s.CodiceSistema == codice);

                foreach (OggettoSistema o in oggettiPerSistema.OggettiSistema)
                {
                    Oggetto x = ctx.Oggettos.Find(o.OggettoIdRiferimento);
                    oggetti.Add(x);
                }
            }

            return oggetti;
        }
    }
}
