using JiraApiOpenSourseLibrary.JiraRestClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace SAU.Services
{
    public class RemovingExcess
    {
        private readonly string _maxLimitIssue = ConfigurationManager.AppSettings["maxLimitIssue"];
        public List<Issue> remove(List<Issue> list)
        {
            List<Issue> removeItemList = new List<Issue>();
            List<int> listNumber = new List<int>();
            int index;
            Random rnd = new Random();
            for (int i = 0; i < Int32.Parse(_maxLimitIssue); i++)
            {
                index = rnd.Next(0, list.Count());
                removeItemList.Add(list[index]);
            }

            return removeItemList;
        }
    }
}