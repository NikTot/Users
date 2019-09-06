﻿using System;

namespace JiraApiOpenSourseLibrary.JiraRestClient
{
    public class IssueType
    {
        public string self { get; set; }
        public string id { get; set; }
        public string description { get; set; }
        public string iconUrl { get; set; }
        public string name { get; set; }
        public bool subtask { get; set; }
        public string avatarId { get; set; }
    }
}
