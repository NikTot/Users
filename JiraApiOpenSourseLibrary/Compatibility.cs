using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace JiraApiOpenSourseLibrary.JiraRestClient
{
    public class JiraClient 
    {
        private readonly IJiraClient<IssueFields> client;
        public JiraClient(string baseUrl, string username, string password)
        {
            client = new JiraClient<IssueFields>(baseUrl, username, password);
        }

        public IEnumerable<Issue> GetIssues(String projectKey)
        {
            return client.GetIssues(projectKey).Select(Issue.From).ToArray();
        }

        public IEnumerable<Issue> GetIssues(String projectKey, String issueType)
        {
            return client.GetIssues(projectKey, issueType).Select(Issue.From).ToArray();
        }

        public IEnumerable<Issue> EnumerateIssues(String projectKey)
        {
            return client.EnumerateIssues(projectKey).Select(Issue.From);
        }

        public IEnumerable<Issue> EnumerateIssues(String projectKey, String issueType)
        {
            return client.EnumerateIssues(projectKey, issueType).Select(Issue.From);
        }

        public IEnumerable<Issue> EnumerateIssuesByQuery(String jqlQuery, String[] fields, Int32 startIndex)
        {
            return client.EnumerateIssuesByQuery(jqlQuery, fields, startIndex).Select(Issue.From);
        }


        public Issue LoadIssue(String issueRef)
        {
            return Issue.From(client.LoadIssue(issueRef));
        }

        public Issue LoadIssue(IssueRef issueRef)
        {
            return Issue.From(client.LoadIssue(issueRef));
        }

        public Issue CreateIssue(String projectKey, String issueType, String summary)
        {
            return Issue.From(client.CreateIssue(projectKey, issueType, summary));
        }

        public Issue CreateIssue(String projectKey, String issueType, IssueFields issueFields)
        {
            return Issue.From(client.CreateIssue(projectKey, issueType, issueFields));
        }

        public Issue UpdateIssue(Issue issue)
        {
            return Issue.From(client.UpdateIssue(issue));
        }

        public void DeleteIssue(IssueRef issue)
        {
            client.DeleteIssue(issue);
        }

        public IEnumerable<Transition> GetTransitions(IssueRef issue)
        {
            return client.GetTransitions(issue);
        }

        public Issue TransitionIssue(IssueRef issue, Transition transition)
        {
            return Issue.From(client.TransitionIssue(issue, transition));
        }

        public IEnumerable<JiraUser> GetWatchers(IssueRef issue)
        {
            return client.GetWatchers(issue);
        }

        public List<History> GetHistories(IssueRef issue)
        {
            return client.GetChangelog(issue);
        }

        public IEnumerable<Comment> GetComments(IssueRef issue)
        {
            return client.GetComments(issue);
        }

        public Comment CreateComment(Issue issue, string comment, Visibility visibility)
        {
            return client.CreateComment(issue, comment, visibility);
        }

        public void DeleteComment(IssueRef issue, Comment comment)
        {
            client.DeleteComment(issue, comment);
        }

        public IEnumerable<Attachment> GetAttachments(IssueRef issue)
        {
            return client.GetAttachments(issue);
        }

        public Attachment CreateAttachment(IssueRef issue, Stream stream, string fileName)
        {
            return client.CreateAttachment(issue, stream, fileName);
        }

        public void DeleteAttachment(Attachment attachment)
        {
            client.DeleteAttachment(attachment);
        }

        public IEnumerable<IssueLink> GetIssueLinks(IssueRef issue)
        {
            return client.GetIssueLinks(issue);
        }

        public IssueLink LoadIssueLink(IssueRef parent, IssueRef child, string relationship)
        {
            return client.LoadIssueLink(parent, child, relationship);
        }

        public IssueLink CreateIssueLink(IssueRef parent, IssueRef child, string relationship)
        {
            return client.CreateIssueLink(parent, child, relationship);
        }

        public void DeleteIssueLink(IssueLink link)
        {
            client.DeleteIssueLink(link);
        }

        public IEnumerable<RemoteLink> GetRemoteLinks(IssueRef issue)
        {
            return client.GetRemoteLinks(issue);
        }

        public RemoteLink CreateRemoteLink(IssueRef issue, RemoteLink remoteLink)
        {
            return client.CreateRemoteLink(issue, remoteLink);
        }

        public RemoteLink UpdateRemoteLink(IssueRef issue, RemoteLink remoteLink)
        {
            return client.UpdateRemoteLink(issue, remoteLink);
        }

        public void DeleteRemoteLink(IssueRef issue, RemoteLink remoteLink)
        {
            client.DeleteRemoteLink(issue, remoteLink);
        }

        public IEnumerable<IssueType> GetIssueTypes()
        {
            return client.GetIssueTypes();
        }

        public ServerInfo GetServerInfo()
        {
            return client.GetServerInfo();
        }


        public void CreateComment()
        {
            throw new NotImplementedException();
        }
    }

    public class Issue : Issue<IssueFields>
    {
        internal static Issue From(Issue<IssueFields> other)
        {
            if (other == null)
                return null;

            return new Issue
            {
                expand = other.expand,
                id = other.id,
                key = other.key,
                self = other.self,
                fields = other.fields,
            };
        }
    }
}
