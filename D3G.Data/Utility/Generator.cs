using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D3G.Data.Utility {
    public class Generator {
        private static bool readyForNextPair = false;
        private static int currentLogAmount = 10;
        private static Dictionary<int, int> balance = initDict();

        public static int NextLogAmount(Random random) {
            if (readyForNextPair) {
                currentLogAmount = random.Next(0, 20);
                readyForNextPair = false;
            } else {
                currentLogAmount = balance[currentLogAmount];
                readyForNextPair = true;
            }
            return currentLogAmount;
        }

        private static Dictionary<int, int> initDict() {
            Dictionary<int, int> d = new Dictionary<int, int>();
            for (int i = 0; i <= 20; ++i) { d.Add(i, 20 - i); }
            return d;
        }
    }
}
