using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D3G.Data.Model {
    public class Trackable {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CacheId { get; set; }

        public static Trackable Generate(int index, int userId, int cacheId) {
            return new Trackable {
                Id = index,
                UserId = userId,
                CacheId = cacheId
            };
        }

        public override string ToString() {
            string s = "({0}, {1}, {2})";
            return string.Format(s, this.Id, this.CacheId, this.UserId);
        }
    }
}
