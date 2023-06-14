using Microsoft.AspNetCore.Mvc;
using CoreLibrary;
using System.Collections.Generic;
using System.IO;

namespace BeeNotepadeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PasekaController : ControllerBase
    {
        private string path = @"..\CoreLibrary\data\";
        private BeehiveStorage beehiveStorage = new BeehiveStorage();

        // GET api/<ManagerController>/menu
        [HttpGet]
        public ActionResult<ICollection<Beehive>> GetAllBeehives([FromHeader] string tgID)
        {
            var files = Directory.GetFiles(path);
            foreach (var file in files)
            {
                if (file.Contains(tgID))
                {
                    beehiveStorage.ReadFromFile(file);
                }

            }

            return beehiveStorage.BeeGarden;
        }

        [HttpPut]
        public ActionResult<Beehive> ChangeBeehive([FromHeader] string tgID, string id, [FromBody]BeehiveObrezka beehiveObrezka)
        {
            var files = Directory.GetFiles(path);
            foreach (var file in files)
            {
                if (file.Contains(tgID))
                {
                    beehiveStorage.ReadFromFile(file);
                }

            }

            var beehive = beehiveStorage.FindById(id);
            beehive.FiledFrames = beehiveObrezka.FiledFrames;
            beehive.Description += $" {beehiveObrezka.Description}";
            return beehive;
        }

    }
}
