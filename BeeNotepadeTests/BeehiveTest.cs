using Microsoft.VisualStudio.TestTools.UnitTesting;
using CoreLibrary;
using System.Collections.Generic;

namespace BeeNotepadeTests
{
    [TestClass]
    public class BeehiveTest
    {
        [TestMethod]
        public void BeehiveConstructorTest()
        {
            Beehive expectedBeehive = new Beehive
            {
                Id = 1,
                Name = "beehive",
                Description = "descrip",
                Image = "img",
                FiledFrames = 7,
                IsLayering = false,
                CheckQuinCells = false,
                IsCull = false,
                CheckBeehive = false,
                DaysForCheck = 4,
                AddDate = System.DateTime.Today
            };

            Beehive actualBeehive = new Beehive($"1;beehive;descrip;img;7;False;" +
                $"False;False;False;4;{System.DateTime.Today}");

            Assert.AreEqual(expectedBeehive.ToString(), actualBeehive.ToString());
        }

        [TestMethod]
        public void BeehiveStorageAddTest()
        {
            var beehiveStorage = new BeehiveStorage();

            var beehive1 = new Beehive("1;beehive1;descrip;img;8;False;False;False;False;0;23.11.2022 0:00:00");
            var beehive2 = new Beehive("2;beehive2;descrip;img;8;False;False;False;False;0;23.11.2022 0:00:00");
            var beehive3 = new Beehive("3;beehive3;descrip;img;8;False;False;False;False;0;23.11.2022 0:00:00");

            var listBeehive = new List<Beehive>{beehive1, beehive2, beehive3};

            beehiveStorage.Add(beehive1);
            beehiveStorage.Add(beehive2);
            beehiveStorage.Add(beehive3);

            CollectionAssert.AreEqual(beehiveStorage.BeeGarden, listBeehive);
        }

        [TestMethod]
        public void BeehiveStorageRemoveTest()
        {
            var beehiveStorage = new BeehiveStorage();

            var beehive1 = new Beehive("1;beehive1;descrip;img;8;False;False;False;False;0;23.11.2022 0:00:00");
            var beehive2 = new Beehive("2;beehive2;descrip;img;8;False;False;False;False;0;23.11.2022 0:00:00");
            var beehive3 = new Beehive("3;beehive3;descrip;img;8;False;False;False;False;0;23.11.2022 0:00:00");

            var listBeehive = new List<Beehive> { beehive1, beehive2 };

            beehiveStorage.Add(beehive1);
            beehiveStorage.Add(beehive2);
            beehiveStorage.Add(beehive3);

            beehiveStorage.RemoveById("3");

            CollectionAssert.AreEqual(beehiveStorage.BeeGarden, listBeehive);
        }

        [TestMethod]
        public void BeehiveStorageChangeTest()
        {
            var beehiveStorage = new BeehiveStorage();

            var beehive1 = new Beehive("1;beehive1;descrip;img;8;False;False;False;False;0;23.11.2022 0:00:00");
            var beehive2 = new Beehive("2;beehive2;descrip;img;8;False;False;False;False;0;23.11.2022 0:00:00");
            
            beehiveStorage.Add(beehive1);
            beehiveStorage.Add(beehive2);

            beehive2.FiledFrames = 10;
            beehive2.Name = "changedBeehive2";
            beehive2.Description = "changedDescrip";
            beehive2.Image = "changedImg";

            var listBeehive = new List<Beehive> { beehive1, beehive2 };

            beehiveStorage.Change(beehive2);

            CollectionAssert.AreEqual(beehiveStorage.BeeGarden, listBeehive);
        }
    }
}
