using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D3G.Data.Model {
    public class Quest {
        public int Id { get; set; }
        public string StartTime { get; set; }
        public int UserId { get; set; }
        public int LogId { get; set; }

        public static Quest Generate(int index, int userId, int logId) {
            return new Quest {
                Id = index,
                StartTime = "now()",
                UserId = userId,
                LogId = logId
            };
        }

        public override string ToString() {
            string s = "({0}, {1}, {2}, {3})";
            return string.Format(s, this.Id, this.StartTime, this.UserId, this.LogId);
        }
    }
}
