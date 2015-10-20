﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D3G.Data.Model {
    public class Hint {
        public int Id { get; set; }
        public string Message { get; set; }
        public bool IsSpoiler { get; set; }
        public int CacheId { get; set; }

        public static Hint Generate(int index, int cacheId) {
            return new Hint {
                Id = index,
                Message = string.Format("message{0}", index),
                IsSpoiler = (index % 2 == 0) ? true : false,
                CacheId = cacheId
            };
        }

        public override string ToString() {
            string s = "({0}, \"{1}\", {2}, {3})";
            return string.Format(s, this.Id, this.Message, this.IsSpoiler.ToString(), this.CacheId);
        }
    }
}
