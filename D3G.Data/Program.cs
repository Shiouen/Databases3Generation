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
                .GenerateDatabase(userAmount : 100000)
                .WriteInserts(cleanUp : true);

            stopwatch.Stop();
            Console.WriteLine(stopwatch.ElapsedMilliseconds);
            Console.Read();
        }
        
        public Program GenerateDatabase(int userAmount) {
            int logAmount = (userAmount + 1) * 10;
            Random random = new Random();

            int userId = 1;
            int questId = 1;
            int cacheId = 1;

            for (int logId = 1; logId < logAmount; ++logId) {

                // change user every 10 logs
                // add user and go to next user
                if (logId % 10 == 0) { 
                    this.Users.Add(User.Generate(userId++));

                    // add a cache to 1/10th of the users
                    // add cache and go to next cache
                    if (userId % 10 == 0) { this.Caches.Add(Cache.Generate(cacheId++, userId, random)); }
                }

                // add log
                //this.Logs.Add(Log.Generate(i, userId, 1));

                // add a quest for half of the logs
                // add quest and go to next quest
                if (logId % 2 == 0) { this.Quests.Add(Quest.Generate(questId++, userId, logId)); }
            }

            return this;
        }

        public void WriteInserts(bool cleanUp) {
            StringBuilder builder = new StringBuilder();
            this.Path = String.Format("{0}../../{1}", AppDomain.CurrentDomain.BaseDirectory, "Files/inserts.sql");

            /* order is IMPORTANT */
            this.Users.BuildAsQuery(builder, "user");
            this.Messages.BuildAsQuery(builder, "message");
            this.Caches.BuildAsQuery(builder, "cache");
            this.CacheAttributes.BuildAsQuery(builder, "cache_attribute");
            this.Attributes.BuildAsQuery(builder, "attribute");
            this.Hints.BuildAsQuery(builder, "hint");
            this.Stages.BuildAsQuery(builder, "stage");
            this.Subscriptions.BuildAsQuery(builder, "subscription");
            this.Trackables.BuildAsQuery(builder, "trackable");
            this.Logs.BuildAsQuery(builder, "log");
            this.LogTrackable.BuildAsQuery(builder, "log_trackable");
            this.Quests.BuildAsQuery(builder, "quest");

            using (StreamWriter writer = new StreamWriter(this.Path)) {
                writer.Write(builder.ToString());
            }
        }
    }
}
