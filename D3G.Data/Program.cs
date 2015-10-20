using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using D3G.Data.Model;
using D3G.Data.Extensions;

namespace D3G.Data {
    public class Program {
        public string Path { get; set; }

        public List<User> Users { get; set; }
        public List<Message> Messages { get; set; }
        public List<Log> Logs { get; set; }
        public List<Quest> Quests { get; set; }
        public List<Trackable> Trackables { get; set; }
        public List<LogTrackable> LogTrackable { get; set; }
        public List<Subscription> Subscriptions { get; set; }
        public List<Cache> Caches { get; set; }
        public List<CacheAttribute> CacheAttributes { get; set; }
        public List<D3G.Data.Model.Attribute> Attributes { get; set; }
        public List<Hint> Hints { get; set; }
        public List<Stage> Stages { get; set; }

        public Program() {
            this.Users = new List<User>();
            this.Messages = new List<Message>();
            this.Logs = new List<Log>();
            this.Quests = new List<Quest>();
            this.Trackables = new List<Trackable>();
            this.LogTrackable = new List<LogTrackable>();
            this.Subscriptions = new List<Subscription>();
            this.Caches = new List<Cache>();
            this.CacheAttributes = new List<CacheAttribute>();
            this.Attributes = new List<D3G.Data.Model.Attribute>();
            this.Hints = new List<Hint>();
            this.Stages = new List<Stage>();
        }

        public static void Main(string[] args) {
            Stopwatch stopwatch = Stopwatch.StartNew();

            new Program()
                .GenerateDatabase(userAmount : 1000000)
                .WriteInserts(cleanUp : true);

            stopwatch.Stop();
            Console.WriteLine(stopwatch.ElapsedMilliseconds);
            Console.Read();
        }
        
        public Program GenerateDatabase(int userAmount) {
            for (int i = 1; i < userAmount + 1; ++i) {
                this.Users.Add(User.Generate(i));
            }

            return this;
        }

        public void WriteInserts(bool cleanUp) {
            StringBuilder builder = new StringBuilder();
            this.Path = String.Format("{0}../../{1}", AppDomain.CurrentDomain.BaseDirectory, "Files/inserts.sql");

            this.Users.BuildAsQuery(builder, "user");

            using (StreamWriter writer = new StreamWriter(this.Path)) {
                writer.Write(builder.ToString());
            }
        }
    }
}
