using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D3G.Data.Model {
    public class Subscription {
        public int Id { get; set; }
        public int CacheId { get; set; }
        public int Channel { get; set; }    // enum

        public static Subscription Generate(int index, int cacheId, Random random) {
            return new Subscription {
                Id = index,
                CacheId = cacheId,
                Channel = random.Next(0,1)
            };
        }

        public override string ToString() {
            string s = "({0}, {1}, {2})";
            return string.Format(s, this.Id, this.CacheId, this.Channel);
        }
    }
}
