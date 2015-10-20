using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D3G.Data.Model {
    public class User {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Street { get; set; }
        public int StreetNumber { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }

        public static User Generate(int index) {
            return new User {
                Id = index,
                FirstName = string.Format("first_name{0}", index),
                LastName = string.Format("last_name{0}", index),
                Street = string.Format("street{0}", index),
                StreetNumber = index,
                City = string.Format("city{0}", index),
                Country = string.Format("country{0}", index),
                Email = string.Format("mail{0}@mail.com", index),
                Telephone = string.Format("+32 0 476 11 22 3333")
            };
        }

        public override string ToString() {
            string s = "({0}, \"{1}\", \"{2}\", \"{3}\", {4}, \"{5}\", \"{6}\", \"{7}\", \"{8}\")";
            return string.Format(s, this.Id, this.FirstName, this.LastName, this.Street, this.StreetNumber, this.City, this.Country, this.Email, this.Telephone);
        }
    }
}
