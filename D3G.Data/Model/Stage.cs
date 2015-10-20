using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D3G.Data.Model {
    public class Stage {
        public int Id { get; set; }
        public int Number { get; set; }
        public string Description { get; set; }
        public StageType Type { get; set; }
        public StageVisibility Visibility { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }

        public static Stage Generate(int index) {
            Random random = new Random();
            return new Stage {
                Id = index,
                Number = random.Next(1,5),
                Description = string.Format("description{0}", index),
                Type = (StageType)random.Next(0,1),
                Visibility = (StageVisibility)random.Next(0,2),
                City = string.Format("city{0}", index),
                Country = string.Format("country{0}", index),
                Longitude = random.NextDouble() * 5,
                Latitude = random.NextDouble() * 5,
            };
        }

        public override string ToString() {
            string s = "({0}, {1}, \"{2}\", {3}, {4}, \"{5}\", \"{6}\", {7}, {8})";
            return string.Format(s, this.Id, this.Number, this.Description, this.Type, this.Visibility, this.City, this.Country, this.Longitude, this.Latitude);
        }

        public enum StageType : int { Physical = 0, Virtual }
        public enum StageVisibility : int { Hidden = 0, Visible, NoCoordinates }
    }
}
