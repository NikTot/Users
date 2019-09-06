using System;

namespace JiraApiOpenSourseLibrary.JiraRestClient
{
    public class Comment
    {
        public string id { get; set; }
        public string body { get; set; }
        public Visibility visibility { get; set; }
    }
}
