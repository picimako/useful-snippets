using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ReleaseTickets
{
    /// <summary>
    /// Given a development project with regular releases and a dedicated Jira Release ticket for tracking the
    /// issues being released.
    /// This class converts the contents of a CSV file (an exported Jira query result), converts the pre-selected fields of them
    /// into Confluence Wiki markup grouped by the issue type.
    /// From a
    /// </summary>
    class ReleasedTicketsMarkupGenerator
    {
        // The index of the fields in the CSV file (might need to be adjusted)
        private const int TYPE = 6, KEY = 7, SUMMARY = 9;

        /// <summary>
        /// The contents of the exported CSV file, including the CSV header which is removed before processing the file.
        /// The CSV delimiter in this case is comma.
        /// If the field that Release numbers are tracked is also filled in for the Release ticket, e.g. the Fix Version/s field,
        /// this method ignores that ticket and will exclude it from the result.
        ///
        /// Given an input (simplified just for demonstration):
        /// ISSUE_TYPE,ISSUE_KEY,ISSUE_SUMMARY,ISSUE_DESCRIPTION
        /// Story,PROJECT-101,"Ticket summary","This is the ticket description"
        /// Bug,PROJECT-404,"[BUG] You screwed up","You screwed up big time."
        ///
        /// The output will be:
        /// h2. Story
        /// * PROJECT-101 - Ticket summary
        ///
        /// h2. Bug
        /// * PROJECT-404 - [BUG] You screwed up
        public string Generate(string exportedCsv)
        {
            List<String> issues = exportedCsv.Split('\n').ToList();
            issues.RemoveAt(0); //remove CSV header
            string markup = "";
            Dictionary<string, string> issuesByType = new Dictionary<string, string>();

            foreach (string issue in issues)
            {
                string[] issueProperties = issue.Split(','); //Split by the CSV delimiter
                if (!issueProperties[TYPE].Equals("Release")) //The current Release ticket is not needed
                {
                    if (issuesByType.ContainsKey(issueProperties[TYPE]))
                    {
                        issuesByType[issueProperties[TYPE]] += JiraTicketFrom(issueProperties);
                    }
                    else
                    {
                        issuesByType.Add(issueProperties[TYPE], JiraTicketFrom(issueProperties));
                    }
                }
            }

            issuesByType.Keys.ToList().ForEach(type => markup += $"h2. {type}\n{issuesByType[type]}\n");
            return markup.TrimEnd();
        }

        /// <summary>
        /// Creates a formatted Jira ticket markup, e.g.:
        /// * PROJECT-404 - [BUG] You screwed up
        /// </summary>
        private string JiraTicketFrom(string[] properties)
        {
            return $"* {properties[KEY]} - {RemoveExtraQuotation(properties[SUMMARY])}\n";
        }

        /// <summary>
        /// Replaces a string like
        /// "Ticket summary called ""I like sushi"" for real"
        /// to
        /// Ticket summary called "I like sushi" for real
        /// </summary>
        private string RemoveExtraQuotation(string summary)
        {
            string noSingleQuotes = Regex.Replace(summary, "\"(?!\")", String.Empty);
            return Regex.Replace(noSingleQuotes, "\"\"", "\"");
        }
    }
}
