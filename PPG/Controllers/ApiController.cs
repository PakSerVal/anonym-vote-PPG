using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using PPG.Models.Entites;
using PPG.Models;


namespace PPG.Controllers
{
    [Route("api/ppg")]
    public class ApiController : Controller
    {
        private Config config;
        public ApiController(Config config)
        {
            this.config = config;
        }

        [HttpPost("handle-bulletins")]
        public IActionResult HandleBulletins ([FromBody] Bulletin[] bulletins)
        {
            if (bulletins.Length != 0)
            {
                Bulletins bulletinsModel = new Bulletins(config);
                DecryptedBulletin[] decryptedBulletins = bulletinsModel.decryptBulletins(bulletins);
                //bulletins.countBulletins(decryptedBulletins);
                return Ok();
            }
            return BadRequest();
        }
    }
}
