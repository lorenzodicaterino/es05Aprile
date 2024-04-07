using es05AprileESA.DTO;
using es05AprileESA.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace es05AprileESA.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SistemaController : Controller
    {
        private readonly SistemaService service;
        public SistemaController(SistemaService servic)
        {
            service = servic;
        }

        [HttpGet]
        public ActionResult StampaTuttiSistemi()
        {
            return Ok(service.GetAllSistema());
        }

        [HttpPost]
        public ActionResult InserisciSistema(SistemaDTO dto)
        {
            if (service.InserisciSistema(dto))
                return Ok();

            return BadRequest();
        }

        [HttpGet("codice/{codice}")]
        public ActionResult RecuperaPerCodice (string codice)
        {
            return Ok(service.GetByCodice(codice));
        }

        [HttpPut("modifica")]
        public ActionResult ModificaSistema (SistemaDTO dto)
        {
            if (service.ModificaSistema(dto))
                return Ok();

            return BadRequest();
        }

        [HttpDelete("elimina/{codice}")]
        public ActionResult EliminaSistema (string codice)
        {
            if (service.EliminaSistema(codice))
                return Ok();


            return BadRequest();
        }

        [HttpGet("oggetti/{cod}")]
        public ActionResult OggettiPerSistema(string cod)
        {
            return Ok(service.RecuperaOggettiPerSistema(cod));
        }
    }
}
