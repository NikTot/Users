using System;
using System.Collections.Generic;

namespace JiraApiOpenSourseLibrary.JiraRestClient
{
    internal class TransitionsContainer
    {
        public string expand { get; set; }

        public List<Transition> transitions { get; set; }
    }
}
