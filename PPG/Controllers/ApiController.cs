using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using PPG.Models.Entities;
using PPG.Models;
using PPG.Data;


namespace PPG.Controllers
{
    [Route("api/ppg")]
    public class ApiController : Controller
    {
        private ElectContext electContext;
        private Config config;

        public ApiController(ElectContext electContext, Config config)
        {
            this.electContext = electContext;
            this.config = config;
        }

        [HttpPost("handle-bulletins")]
        public IActionResult HandleBulletins ([FromBody] Bulletin[] bulletins)
        {
            if (bulletins.Length != 0)
            {
                Bulletins bulletinsModel = new Bulletins(electContext, config);
                DecryptedBulletin[] decryptedBulletins = bulletinsModel.decryptBulletins(bulletins);
                bulletinsModel.saveBulletins(decryptedBulletins);
                return Ok();
            }
            return BadRequest();
        }
    }
}
