using Microsoft.AspNetCore.Mvc;
using CoreLibrary;
using System.Collections.Generic;

namespace BeeNotepadeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PasekaController : ControllerBase
    {
        private string menuPath = @"..\CoreLibrary\data\menu.txt";
        private BeehiveStorage menuStorage = new BeehiveStorage();

        // GET api/<ManagerController>/menu
        [HttpGet]
        public ActionResult<ICollection<Beehive>> GetAllMenu()
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

        // POST api/<ManagerController>
        [HttpPost]
        public ActionResult Post([FromBody] Beehive menu)
        {
            try
            {
                menuStorage.ReadFromFile(menuPath);
                menuStorage.Add(menu);
                menuStorage.WriteInFile(menuPath);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        // PUT api/<ManagerController>
        [HttpPut("{id}")]
        public ActionResult<Beehive> Put(string id, [FromBody] Beehive menu)
        {
            menuStorage.ReadFromFile(menuPath);
            menu.Id = int.Parse(id);
            if (menuStorage.FindById(id) == null)
                return BadRequest();
            try
            {
                menuStorage.Change(menu);
                menuStorage.WriteInFile(menuPath);
                return Ok(menuStorage.FindById(id));
            }
            catch
            {
                return BadRequest();
            }
        }

        // DELETE api/<ManagerController>
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            menuStorage.ReadFromFile(menuPath);
            var menu = menuStorage.FindById(id);
            if (menu == null)
                return BadRequest();
            menuStorage.RemoveById(id);
            menuStorage.WriteInFile(menuPath);
            return Ok();
        }
    }
}
