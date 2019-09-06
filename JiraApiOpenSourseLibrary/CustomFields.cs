using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JiraApiOpenSourseLibrary
{
    public class Customfield13941
    {
        public string id { get; set; }
        public string name { get; set; }
        public Links _links { get; set; }
        public List<CompletedCycle> completedCycles { get; set; }
        public OngoingCycle ongoingCycle { get; set; }
    }

    public class Customfield13041
    {
        public string id { get; set; }
        public string name { get; set; }
        public Links _links { get; set; }
        public List<CompletedCycle> completedCycles { get; set; }
        public OngoingCycle ongoingCycle { get; set; }
    }

    public class Customfield13042
    {
        public string id { get; set; }
        public string name { get; set; }
        public Links _links { get; set; }
        public List<CompletedCycle> completedCycles { get; set; }
        public OngoingCycle ongoingCycle { get; set; }
    }

    public class OngoingCycle
    {
        public StartTime startTime { get; set; }
        public BreachTime breachTime { get; set; }
        public bool breached { get; set; }
        public bool paused { get; set; }
        public bool withinCalendarHours { get; set; }
        public GoalDuration goalDuration { get; set; }
        public ElapsedTime elapsedTime { get; set; }
        public RemainingTime remainingTime { get; set; }
    }

    public class BreachTime
    {
        public string iso8601 { get; set; }
        public string friendly { get; set; }
        public long epochMillis { get; set; }
    }

    public class CompletedCycle
    {
        public StartTime startTime { get; set; }
        public StopTime stopTime { get; set; }
        public bool breached { get; set; }
        public GoalDuration goalDuration { get; set; }
        public ElapsedTime elapsedTime { get; set; }
        public RemainingTime remainingTime { get; set; }
    }

    public class Links
    {
        public string self { get; set; }
    }

    public class StartTime
    {
        public string iso8601 { get; set; }
        public string friendly { get; set; }
        public long epochMillis { get; set; }
    }

    public class StopTime
    {
        public string iso8601 { get; set; }
        public string friendly { get; set; }
        public long epochMillis { get; set; }
    }

    public class GoalDuration
    {
        public long millis { get; set; }
        public string friendly { get; set; }
    }

    public class ElapsedTime
    {
        public long millis { get; set; }
        public string friendly { get; set; }
    }

    public class RemainingTime
    {
        public long millis { get; set; }
        public string friendly { get; set; }
    }
}
