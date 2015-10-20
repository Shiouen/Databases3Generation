﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D3G.Data.Model {
    public class LogTrackable {
        public int LogId { get; set; }
        public int TrackableId { get; set; }
        public bool IsTakenOut { get; set; }

        public static LogTrackable Generate(int index, int logId, int trackableId) {
            Random random = new Random();
            bool isTakenOut = false;

            if (random.NextDouble() >= 0.5) { isTakenOut = true; }

            return new LogTrackable {
                LogId = logId,
                TrackableId = trackableId,
                IsTakenOut = isTakenOut,
            };
        }

        public override string ToString() {
            string s = "({0}, {1}, {2})";
            return string.Format(s, this.LogId, this.TrackableId, this.IsTakenOut.ToString());
        }
    }
}