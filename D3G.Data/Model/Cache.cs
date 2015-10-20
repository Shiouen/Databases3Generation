using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D3G.Data.Model {
    public class Cache {
        public int Id { get; set; }
        public CacheDifficulty Difficulty { get; set; }
        public CacheTerrain Terrain { get; set; }
        public CacheSize Size { get; set; }
        public CacheType Type { get; set; }
        public string Instructions { get; set; }
        public int UserId { get; set; }

        public static Cache Generate(int index, int userId) {
            Random random = new Random();
            return new Cache {
                Id = index,
                Difficulty = (CacheDifficulty) random.Next(0,4),
                Terrain = (CacheTerrain) random.Next(0,4),
                Size = (CacheSize) random.Next(0,3),
                Type = (CacheType) random.Next(0,1),
                Instructions = string.Format("instruction{0}", index),
                UserId = userId
            };
        }

        public override string ToString() {
            string s = "({0}, {1}, {2}, {3}, {4}, \"{5}\", {6})";
            return string.Format(s, this.Id, this.Difficulty, this.Terrain, this.Size, this.Type, this.Instructions, this.UserId);
        }
    }

    public enum CacheTerrain : int { Easy = 0, Medium, Hard, Heroic, Legendary }
    public enum CacheDifficulty : int { Easy = 0, Medium, Hard, Heroic, Legendary }
    public enum CacheSize : int { Nano = 0, Micro, Regular, Huge }
    public enum CacheType : int { Single = 0, Multi }
}
