using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D3G.Data.Model {
    public class CacheAttribute {
        public int AttributeId { get; set; }
        public int CacheId { get; set; }

        public static CacheAttribute Generate(int attributeId, int cacheId) {
            return new CacheAttribute {
                AttributeId = attributeId,
                CacheId = cacheId
            };
        }

        public override string ToString() {
            string s = "({0}, {1})";
            return string.Format(s, this.AttributeId, this.CacheId);
        }
    }
}
