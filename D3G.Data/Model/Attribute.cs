using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D3G.Data.Model {
    public class Attribute {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public static Attribute Generate(int index) {
            return new Attribute {
                Id = index,
                Name = string.Format("name{0}", index),
                Description = string.Format("description{0}", index)
            };
        }

        public override string ToString() {
            string s = "({0}, \"{1}\", \"{2}\")";
            return string.Format(s, this.Id, this.Name, this.Description);
        }
    }
}
