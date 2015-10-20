using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D3G.Data.Model {
    public class Message {
        public int Id { get; set; }
        public string Text { get; set; }
        public int UserId { get; set; }

        public static Message Generate(int index, int userId) {
            return new Message {
                Id = index,
                Text = string.Format("text{0}", index),
                UserId = userId
            };
        }

        public override string ToString() {
            string s = "({0}, \"{1}\", \"{2}\")";
            return string.Format(s, this.Id, this.Text, this.UserId);
        }
    }
}
