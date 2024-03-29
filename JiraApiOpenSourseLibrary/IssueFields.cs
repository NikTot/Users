﻿using System;
using System.Collections.Generic;

namespace JiraApiOpenSourseLibrary.JiraRestClient
{
    public class IssueFields
    {
        public IssueFields()
        {
            status = new Status();
            timetracking = new Timetracking();
            labels = new List<String>();
            comments = new List<Comment>();
            issuelinks = new List<IssueLink>();
            attachment = new List<Attachment>();
            watchers = new List<JiraUser>();
        }

        public String summary { get; set; }
        public String description { get; set; }
        
        public DateTime created { get; set; }
        public DateTime updated { get; set; }

        public DateTime? resolutiondate { get; set; }

        public Priority priority { get; set; }
        public IssueType issuetype { get; set; }
        public Timetracking timetracking { get; set; }
        public Status status { get; set; }
        public Customfield13941 customfield_13941 { get; set; }
        public Customfield13041 customfield_13041 { get; set; }
        public Customfield13042 customfield_13042 { get; set; }

        public String customfield_15340 { get; set; }

        public JiraUser reporter { get; set; }
        public JiraUser assignee { get; set; }
        public List<JiraUser> watchers { get; set; } 

        public List<String> labels { get; set; }
        public List<Comment> comments { get; set; }
        public List<IssueLink> issuelinks { get; set; }
        public List<Attachment> attachment { get; set; }
    }
}
