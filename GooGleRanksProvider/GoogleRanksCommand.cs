using System;
using SearchCommands;
using GoogleCustomSearchProvider;
using Google.Apis.Customsearch.v1.Data;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace GoogleRanksProvider
{
    /// <summary>
    /// Class holds/acts upon the required input for ranks provider
    /// </summary>
    public class GoogleRanksCommand : ISearchRanksCommand
    {
        #region private members

        private string _keyWords = string.Empty;
        private string _urlToSearch = string.Empty;
        private int _numberofResultsToSearchFor = 100;

        #endregion

        #region properties
        /// <summary>
        /// Keywords used for searching entire web.
        /// </summary>
        public string KeyWords
        {
            get
            {
                return _keyWords;
            }

            set
            {
                _keyWords = value;
            }
        }

        /// <summary>
        /// Number determining the results to look for. Eg : If it is 100, only top 100 results will be parsed.
        /// </summary>
        public int NumberOfResultsToSearchFor
        {
            get
            {
                return _numberofResultsToSearchFor;
            }
            set
            {
                _numberofResultsToSearchFor = value;
            }
          
        }

        /// <summary>
        /// URL that needs to be searched within results.
        /// </summary>
        public string URLToSearch
        {
            get
            {
                return _urlToSearch;
            }

            set
            {
                _urlToSearch = value;
            }
        }

        #endregion

        #region Constructor

        public GoogleRanksCommand(string KeyWordsToSearchWeb,string URLToSearchWithinResults,int NumberOfResultsToSearchFor = 100)
        {
            _keyWords = KeyWordsToSearchWeb;
            _urlToSearch = URLToSearchWithinResults;
            _numberofResultsToSearchFor = NumberOfResultsToSearchFor;
        }

        public string Execute(int numBerOfResultToLookFor = 100)
        {
            string ranks = string.Empty;
            string apiKey = ConfigurationManager.AppSettings["ApiKey"];
            string searchEngineKey = ConfigurationManager.AppSettings["EngineKey"];

            if (!string.IsNullOrEmpty(apiKey) && !string.IsNullOrEmpty(searchEngineKey))
                GoogleCustomSearchProvider.GoogleCustomSearchProvider.Instance.Initialize(apiKey, searchEngineKey);
            else
            {
                Console.WriteLine("Could not find the api key, search engine key.Please add them to the application configuration.");
                return null;
            }

            IList<Result> resultSet = GoogleCustomSearchProvider.GoogleCustomSearchProvider.Instance.ExecuteSearch(_keyWords, numBerOfResultToLookFor);
            if(resultSet != null)
            {
                ranks = ParseResultsForTheURL(_urlToSearch, resultSet);
            }
            return ranks;
        }

        private string ParseResultsForTheURL(string URL,IList<Result> resultSet)
        {
            StringBuilder result = new StringBuilder();
            try
            {
                int count = 1;
                if (resultSet != null)
                    foreach (var item in resultSet)
                    {
                        count++;

                        if (item.DisplayLink.Contains(URL) || item.FormattedUrl.Contains(URL) || item.FormattedUrl.Contains(URL)
                            || item.Link.Contains(URL))
                        {
                            Console.WriteLine("Found within the below result item : ");
                            Console.WriteLine("Link - > " + item.Link);
                            Console.WriteLine("Title - > " + item.Title);
                            Console.WriteLine("DisplayLink - > " + item.DisplayLink);
                            Console.WriteLine("URL - > " + item.FormattedUrl);
                            result.Append(count + ",");
                        }
                    }
                if (string.IsNullOrEmpty(result.ToString()))
                {
                    result.Append("0");
                }
                else
                {
                    result.Remove(result.Length - 1, 1); // removes last ","
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Could not parse the results : ");
                Console.WriteLine(ex.Message);
            }
               
            return result.ToString();
        }

        #endregion
    }
}
