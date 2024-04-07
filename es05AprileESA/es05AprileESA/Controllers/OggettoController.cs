using es05AprileESA.DTO;
using es05AprileESA.Services;
using Microsoft.AspNetCore.Mvc;

namespace es05AprileESA.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OggettoController : Controller
    {
        private readonly OggettoService service;
        public OggettoController(OggettoService servic)
        {
            service = servic;
        }

        [HttpGet]
        public ActionResult StampaTuttiOggetti()
        {
            return Ok(service.GetAllOggetti());
        }

        [HttpPost]
        public ActionResult InserisciOggetto(OggettoDTO dto)
        {
            if (service.InserisciOggetto(dto))
                return Ok();

            return BadRequest();
        }

        [HttpGet("codice/{codice}")]
        public ActionResult RecuperaPerCodice(string codice)
        {
            return Ok(service.GetByCodiceOggetto(codice));
        }

        [HttpPut("modifica")]
        public ActionResult ModificaOggetto(OggettoDTO dto)
        {
            if (service.ModificaOggetto(dto))
                return Ok();

            return BadRequest();
        }

        [HttpDelete("elimina/{codice}")]
        public ActionResult EliminaOggetto(string codice)
        {
            if (service.EliminaOggetto(codice))
                return Ok();

            return BadRequest();
        }

        [HttpGet("sistemi/{cod}")]
        public ActionResult SistemiPerOggetti(string cod)
        {
            return Ok(service.RecuperaSistemiPerOggetto(cod));
        }

        [HttpGet("sistemi/{codO}/{codS}")]
        public ActionResult InserisciInSistema(string codO, string codS)
        {
            if(service.InserisciInSistema(codO, codS))
                return Ok();

            return BadRequest();
        }
    }
}
