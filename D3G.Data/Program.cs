using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using D3G.Data.Extensions;
using D3G.Data.Model;
using D3G.Data.Utility;

namespace D3G.Data {
    public class Program {
        public string Path { get; set; }
        private List<Tuple<string, string>> places;

        public List<User> Users { get; set; }
        public List<Message> Messages { get; set; }
        public List<Log> Logs { get; set; }
        public List<Quest> Quests { get; set; }
        public List<Trackable> Trackables { get; set; }
        public List<LogTrackable> LogTrackables { get; set; }
        public List<Subscription> Subscriptions { get; set; }
        public List<Cache> Caches { get; set; }
        public List<CacheAttribute> CacheAttributes { get; set; }
        public List<Model.Attribute> Attributes { get; set; }
        public List<Hint> Hints { get; set; }
        public List<Stage> Stages { get; set; }

        public Program() {
            this.Users = new List<User>();
            this.Messages = new List<Message>();
            this.Logs = new List<Log>();
            this.Quests = new List<Quest>();
            this.Trackables = new List<Trackable>();
            this.LogTrackables = new List<LogTrackable>();
            this.Subscriptions = new List<Subscription>();
            this.Caches = new List<Cache>();
            this.CacheAttributes = new List<CacheAttribute>();
            this.Attributes = new List<Model.Attribute>();
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
            Random random = new Random();

            this.initPlaces();

            int cacheAmount = userAmount / 10;
            int logAmount = userAmount * 10;

            int cacheId = 1;
            int hintId = 1;
            int logId = 1;
            int logTrackableId = 1;
            int messageId = 1;
            int questId = 1;
            int stageId = 1;
            int stageNumber = 1;
            int subscriptionId = 1;
            int trackableId = 1;
            int userId = 1;

            // logs, users, quests and caches
            for (logId = 1; logId <= logAmount; ++logId) {
                // add a quest for half of the logs
                // add quest and go to next quest
                if (logId % 2 == 0) { this.Quests.Add(Quest.Generate(questId++, userId, logId)); }

                // add a subscription with current user (which switches every 10 logs) and random cache
                // only for even logIds, so #logs/2 = 5 subscriptions made per user
                // add subscription and go to next subscription
                if (logId % 2 != 0) {
                    this.Subscriptions.Add(Subscription.Generate(subscriptionId++,
                        random.Next(1, cacheAmount + 1),
                        userId, random));
                }

                // add log
                this.Logs.Add(Log.Generate(logId, userId, cacheId, random));

                // add trackable for 1/1000th of logs (resulting in 1000 trackables)
                // add trackable with random current user and cache, and got to next cache
                if (logId % 1000 == 0) { this.Trackables.Add(Trackable.Generate(trackableId++, userId, cacheId)); }

                // change user every 10 logs
                // add user and go to next user
                if (logId % 10 == 0) {
                    this.Users.Add(User.Generate(userId));

                    // add a cache to 1/10th of the users
                    // add cache and go to next cache
                    if (userId % 10 == 0) { 
                        this.Caches.Add(Cache.Generate(cacheId, userId, random));

                        // semi randomised adding of hints
                        // add hint and go to next hint
                        if (userId % 5 == 0) {
                            this.Hints.Add(Hint.Generate(hintId++, cacheId, random));
                        }
                        if (userId % 3 == 0) {
                            this.Hints.Add(Hint.Generate(hintId++, cacheId, random));
                            this.Hints.Add(Hint.Generate(hintId++, cacheId, random));
                        }

                        // semi randomised adding of cacheattributes
                        if (cacheId % 6 == 0) {
                            this.CacheAttributes.Add(CacheAttribute.Generate(random.Next(1,5), cacheId));
                        }
                        if (cacheId % 7 == 0) {
                            this.CacheAttributes.Add(CacheAttribute.Generate(random.Next(5, 8), cacheId));
                            this.CacheAttributes.Add(CacheAttribute.Generate(random.Next(8, 11), cacheId));
                        }

                        ++cacheId;
                    }

                    // add a message to 1/100th of the users
                    // add message and go to next message
                    if (userId % 100 == 0) { this.Messages.Add(Message.Generate(messageId++, userId)); }

                    ++userId;
                }
            }

            // each cache gets its own stage and multicaches get a variable amount of stages
            // also set attributes for stages in the mean time
            foreach (Cache c in this.Caches) {
                stageNumber = 1;

                if (c.Type == 1) {
                    if (stageId % 5 == 0) {
                        this.Stages.Add(Stage.Generate(stageId, stageNumber++, null, stageId++, this.places, random));
                    }
                    if (stageId % 3 == 0) {
                        this.Stages.Add(Stage.Generate(stageId, stageNumber++, null, stageId++, this.places, random));
                        this.Stages.Add(Stage.Generate(stageId, stageNumber++, null, stageId++, this.places, random));
                    }
                }

                this.Stages.Add(Stage.Generate(stageId++, stageNumber, c.Id, null, this.places, random));

            }

            // add attributes
            for (int attributeId = 1; attributeId <= 10; ++attributeId) {
                this.Attributes.Add(Model.Attribute.Generate(attributeId));
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
            this.Attributes.BuildAsQuery(builder, "attribute");
            this.CacheAttributes.BuildAsQuery(builder, "cache_attribute");
            this.Hints.BuildAsQuery(builder, "hint");
            this.Stages.BuildAsQuery(builder, "stage");
            this.Subscriptions.BuildAsQuery(builder, "subscription");
            this.Trackables.BuildAsQuery(builder, "trackable");
            this.Logs.BuildAsQuery(builder, "log");
            this.LogTrackables.BuildAsQuery(builder, "log_trackable");
            this.Quests.BuildAsQuery(builder, "quest");

            using (StreamWriter writer = new StreamWriter(this.Path)) {
                writer.Write(builder.ToString());
            }
        }

        private Program initPlaces() {
            this.places = new List<Tuple<string, string>>();
            string[] countries = { "Belgium", "France", "Indonesia", "Japan", "United Kingdom" };

            places.Add(countries[0], "Antwerp");
            places.Add(countries[0], "Brugge");
            places.Add(countries[0], "Brussels");
            places.Add(countries[0], "Limburg");
            places.Add(countries[0], "Ghent");

            places.Add(countries[1], "Paris");
            places.Add(countries[1], "Lille");
            places.Add(countries[1], "Bordeaux");
            places.Add(countries[1], "Versailles");
            places.Add(countries[1], "Nice");

            places.Add(countries[2], "Jakarta");
            places.Add(countries[2], "Semarang");
            places.Add(countries[2], "Banjarbaru");
            places.Add(countries[2], "Makassar");
            places.Add(countries[2], "Bira");

            places.Add(countries[3], "Tokyo");
            places.Add(countries[3], "Osaka");
            places.Add(countries[3], "Kyoto");
            places.Add(countries[3], "Koyasan");
            places.Add(countries[3], "Hiroshima");

            places.Add(countries[4], "London");
            places.Add(countries[4], "Hatfield");
            places.Add(countries[4], "Manchester");
            places.Add(countries[4], "Birmingham");
            places.Add(countries[4], "Gloucestershire");

            return this;
        }
    }
}
