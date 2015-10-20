﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D3G.Data.Model {
    public class Cache {
        public int Id { get; set; }
        public int Difficulty { get; set; } // enum
        public int Terrain { get; set; }    // enum
        public int Size { get; set; }       // enum
        public string Instructions { get; set; }
        public int Type { get; set; }       // enum
        public int UserId { get; set; }

        public static Cache Generate(int index, int userId, Random random) {
            return new Cache {
                Id = index,
                Difficulty = random.Next(0,4),
                Terrain = random.Next(0,4),
                Size = random.Next(0,3),
                Instructions = string.Format("instruction{0}", index),
                Type = random.Next(0, 1),
                UserId = userId
            };
        }

        public override string ToString() {
            string s = "({0}, {1}, {2}, {3}, \"{4}\", {5}, {6})";
            return string.Format(s, this.Id, this.Difficulty, this.Terrain, this.Size, this.Instructions, this.Type, this.UserId);
        }
    }
}
