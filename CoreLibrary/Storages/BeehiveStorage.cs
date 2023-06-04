using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CoreLibrary
{
    public interface IBeehiveStorage
    {
        void Add(Beehive beehive);
        void RemoveById(string id);
        void ReadFromFile(string path);
        void WriteInFile(string path);
    }
    public class BeehiveStorage : IBeehiveStorage
    {
        public List<Beehive> BeeGarden { get; private set; }

        public BeehiveStorage()
        {
            BeeGarden = new List<Beehive>();
        }

        public void Add(Beehive beehive)
        {
            if (BeeGarden.Count != 0)
                beehive.Id = BeeGarden.Last().Id + 1;
            else beehive.Id = 1;
            beehive.CheckQuinCells = true;
            beehive.IsCull = true;
            beehive.CheckBeehive = false;
            beehive.DaysForCheck = CountDaysForCheck(beehive).DaysForCheck;
            beehive.AddDate = DateTime.Today;
            BeeGarden.Add(beehive);
        }

        public void Change(Beehive beehive)
        {
            Beehive chBeehive = BeeGarden.Find(m => m.Id == beehive.Id);
            if (chBeehive.CheckQuinCells != beehive.CheckQuinCells ||
                chBeehive.IsCull != beehive.IsCull ||
                chBeehive.CheckBeehive != beehive.CheckBeehive)
                chBeehive.AddDate = DateTime.Today;
            chBeehive.Name = beehive.Name;
            chBeehive.Description = beehive.Description;
            chBeehive.Image = beehive.Image;
            chBeehive.FiledFrames = beehive.FiledFrames;
            chBeehive.CheckQuinCells = beehive.CheckQuinCells;
            chBeehive.IsCull = beehive.IsCull;
            chBeehive.CheckBeehive = beehive.CheckBeehive;
            chBeehive.DaysForCheck = CountDaysForCheck(chBeehive).DaysForCheck;
        }

        public Beehive CountDaysForCheck(Beehive beehive)
        {
            if (beehive.IsLayering)
                beehive.DaysForCheck = 4;
            if (beehive.CheckQuinCells)
                beehive.DaysForCheck = 1;
            if (beehive.IsCull)
                beehive.DaysForCheck = 1;
            if (beehive.CheckBeehive)
                beehive.DaysForCheck = 16;
            return beehive;
        }

        public void RemoveById(string id)
        {
            BeeGarden.RemoveAll(m => m.Id == int.Parse(id));
        }

        public Beehive FindById(string id)
        {
            return BeeGarden.Find(m => m.Id == int.Parse(id));
        }

        public void ReadFromFile(string path)
        {
            BeeGarden.Clear();

            try
            {
                using (var sr = new StreamReader(path))
                {
                    string str;
                    while ((str = sr.ReadLine()) != null)
                    {
                        BeeGarden.Add(new Beehive(str));
                    }
                }
            }
            catch (Exception) { }
        }

        public void WriteInFile(string path)
        {
            
            using (var sw = new StreamWriter(path, false))
            {
                foreach (var beehive in BeeGarden)
                {
                    sw.WriteLine(beehive);
                }
            }
        }
    }
}
