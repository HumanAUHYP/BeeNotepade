using Microsoft.AspNetCore.Mvc;
using CoreLibrary;
using System.Collections.Generic;

namespace BeeNotepadeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PasekaController : ControllerBase
    {
        private string menuPath = @"..\CoreLibrary\data\";
        private BeehiveStorage menuStorage = new BeehiveStorage();

        // GET api/<ManagerController>/menu
        [HttpGet]
        public ActionResult<ICollection<Beehive>> GetAllBeehive()
        {
            menuStorage.ReadFromFile(menuPath);
            return menuStorage.BeeGarden;
        }

        // GET api/<ManagerController>/menu
        [HttpGet("{Id}")]
        public ActionResult<Beehive> GetMenuById(string id)
        {
            menuStorage.ReadFromFile(menuPath);
            return menuStorage.FindById(id);
        }

       

    }
}
