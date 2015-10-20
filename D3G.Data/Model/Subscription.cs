using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D3G.Data.Model {
    public class Subscription {
        public int Id { get; set; }
        public int CacheId { get; set; }
        public SubscriptionChannel Channel { get; set; }

        public static Subscription Generate(int index, int cacheId) {
            Random random = new Random();
            return new Subscription {
                Id = index,
                CacheId = cacheId,
                Channel = (SubscriptionChannel) random.Next(0,1)
            };
        }

        public override string ToString() {
            string s = "({0}, {1}, {2})";
            return string.Format(s, this.Id, this.CacheId, this.Channel);
        }

        public enum SubscriptionChannel : int { Email = 0, SMS }
    }
}
