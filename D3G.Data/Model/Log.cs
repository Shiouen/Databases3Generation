using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D3G.Data.Model {
    public class Log {
        public int Id { get; set; }
        public LogType Type { get; set; }
        public string Message { get; set; }
        public string Time { get; set; }
        public int UserId { get; set; }
        public int CacheId { get; set; }

        public static Log Generate(int index, int userId, int cacheId) {
            Random random = new Random();
            return new Log {
                Id = index,
                Type = (LogType)random.Next(0,2),
                Message = string.Format("message{0}", index),
                Time = "now()",
                UserId = userId,
                CacheId = cacheId
            };
        }

        public override string ToString() {
            string s = "({0}, {1}, \"{2}\", {3}, {4}, {5})";
            return string.Format(s, this.Id, this.Type, this.Message, this.Time, this.UserId, this.CacheId);
        }
    }

    public enum LogType : int { Found = 0, NotFound, GeneralMessage }
}
