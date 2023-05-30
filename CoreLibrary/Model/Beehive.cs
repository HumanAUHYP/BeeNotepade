using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreLibrary
{
    public class Beehive
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public int FiledFrames { get; set; }
        public bool IsLayering { get; set; }
        public bool CheckQuinCells { get; set; }
        public bool IsCull { get; set; }
        public bool CheckBeehive { get; set; }
        public int DaysForCheck { get; set; }
        public DateTime AddDate { get; set; }



        public Beehive() { }

        public Beehive(string str)
        {
            var data = str.Split(';');
            Id = int.Parse(data[0]);
            Name = data[1];
            Description = data[2];
            Image = data[3];
            FiledFrames = int.Parse(data[4]);
            IsLayering = bool.Parse(data[5]);
            CheckQuinCells = bool.Parse(data[6]);
            IsCull = bool.Parse(data[7]);
            CheckBeehive = bool.Parse(data[8]);
            DaysForCheck = int.Parse(data[9]);
            AddDate = DateTime.Parse(data[10]);

        }

        public override string ToString()
        {
            return $"{Id};{Name};{Description};{Image};{FiledFrames};{IsLayering};{CheckQuinCells};{IsCull};{CheckBeehive};{DaysForCheck};{AddDate}";
        }
    }
}
