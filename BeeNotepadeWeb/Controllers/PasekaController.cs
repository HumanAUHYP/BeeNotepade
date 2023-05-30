using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using CoreLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeeNotepadeWeb.Controllers
{
    public class PasekaController : Controller
    {
        private string path = @"..\CoreLibrary\data\menu.txt";

        // ссылка на объект - хранилище ульев
        BeehiveStorage beehiveStorage;

        public PasekaController(IWebHostEnvironment _environment, IMenuStorage _menuStorage)
        {
            beehiveStorage = (BeehiveStorage)_menuStorage;
        }
        public IActionResult Index()
        {
            beehiveStorage.ReadFromFile(path);
            var today = DateTime.Today;
            foreach (Beehive beehive in beehiveStorage.BeeGarden)
            {
                beehive.DaysForCheck -= (int)Math.Round((today - beehive.AddDate).TotalDays);
                if (beehive.DaysForCheck < 0)
                    beehive.DaysForCheck = 0;
            }
            var beeGarden = beehiveStorage.BeeGarden;
            
            return View(beeGarden);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Beehive beehive)
        {
            beehiveStorage.Add(beehive);
            beehiveStorage.WriteInFile(path);
            return RedirectToAction("Index");
        }

        public IActionResult Change(string id)
        {
            return View(beehiveStorage.FindById(id));
        }

        [HttpPost]
        public IActionResult Change(Beehive beehive)
        {
            beehiveStorage.Change(beehive);
            beehiveStorage.WriteInFile(path);
            return RedirectToAction("Index");
        }
        public IActionResult Remove(string id)
        {
            beehiveStorage.RemoveById(id);
            beehiveStorage.WriteInFile(path);
            return RedirectToAction("Index");
        }
    }
}
