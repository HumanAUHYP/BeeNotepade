﻿using System;
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
            beehive.AddDate = DateTime.Today;
            beehive.DaysForCheck = CountDaysForCheck(beehive).DaysForCheck;
            BeeGarden.Add(beehive);
        }
        public void AddOffshoot(Beehive beehive)
        {
            if (BeeGarden.Count != 0)
                beehive.Id = BeeGarden.Last().Id + 1;
            else beehive.Id = 1;
            beehive.CheckQuinCells = false;
            beehive.IsCull = false;
            beehive.CheckBeehive = false;
            beehive.AddDate = DateTime.Today;
            beehive.DaysForCheck = CountDaysForCheck(beehive).DaysForCheck;
            BeeGarden.Add(beehive);
        }

        public void ChoiceBeehive(string id)
        {
            Beehive chBeehive = BeeGarden.Find(m => m.Id == Convert.ToInt32(id));
            chBeehive.FiledFrames -= 6;
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
            chBeehive.CheckBeehive = CountDaysForCheck(chBeehive).CheckBeehive;
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
            {
                beehive.DaysForCheck = 16;
                beehive.CheckBeehive = false;
            }
                
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
