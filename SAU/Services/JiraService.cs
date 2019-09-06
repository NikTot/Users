using JiraApiOpenSourseLibrary.JiraRestClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using SAU.DTO;
using System.IO;
using NLog;

namespace SAU.Services
{
    public class JiraService
    {
        private readonly string _jiraUrl = ConfigurationManager.AppSettings["jiraUrl"];
        private readonly string _jiraUserName = ConfigurationManager.AppSettings["jiraUserName"];
        private readonly string _jiraPassword = ConfigurationManager.AppSettings["jiraPassword"];
        private readonly string _clearServiceUrl = ConfigurationManager.AppSettings["clearServiceUrl"];
        private JiraClient _jiraClient;
        private ClearTextService _clearText;
        private RemovingExcess _removingExcess;
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public JiraService()
        {
            _jiraClient = new JiraClient(_jiraUrl, _jiraUserName, _jiraPassword);
            _clearText = new ClearTextService();
            _removingExcess = new RemovingExcess();
        }

        public void Get(IList<JQLFilterDTO> jqlFilters)
        {
            if (jqlFilters == null || jqlFilters.Any())
            {
                try
                {
                    string path = AppDomain.CurrentDomain.BaseDirectory + "Files/";
                    Directory.CreateDirectory(path);
                    using (var file = new StreamWriter(path + "Azure.csv"))
                    {
                        foreach (var jqlFilter in jqlFilters)
                        {
                            _logger.Info($"Наичнаю получать тикеты по фильтру {jqlFilter.System.Name}");
                            var issues = _jiraClient.EnumerateIssuesByQuery(jqlFilter.JqlString, null, 0).ToList();
                            issues = _removingExcess.remove(issues);
                            _logger.Info($"Получил из фильтра {jqlFilter.System.Name} тикетов {issues.Count()}");
                            using (var fileGrader = new StreamWriter(path + jqlFilter.System.Name + ".csv"))
                            {
                                _logger.Info($"Начинаю запись в файл {jqlFilter.System.Name}.csv");
                                foreach (var issue in issues)
                                {                                    
                                    var number = issue.key;
                                    var summary = _clearText.CleanUp(issue.fields.summary, _clearServiceUrl);
                                    var description = _clearText.CleanUp(issue.fields.description, _clearServiceUrl);
                                    var text = summary + " " + description;
                                    string line = $"{jqlFilter.System.Name};{text}";
                                    file.WriteLine(line);
                                    string lineForGrader = $"{jqlFilter.System.Name};{number};{summary};{description}";
                                    fileGrader.WriteLine(lineForGrader);
                                }
                                _logger.Info($"Закончил запись в файл {jqlFilter.System.Name}.csv");
                            }                            
                        }
                        _logger.Info($"Закончил запись в файл Azure.csv");
                    }
                }
                catch (Exception ex)
                {
                    _logger.Error(ex, "Ошибка при записи данных в файл");
                }
            }
        }
    }
}