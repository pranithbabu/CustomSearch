using Google.Apis.Customsearch.v1;
using Google.Apis.Customsearch.v1.Data;
using Google.Apis.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleCustomSearchProvider
{
    /// <summary>
    /// Single instance used for performing the actual google search to fetch results.
    /// </summary>
    public sealed class GoogleCustomSearchProvider
    {
        #region private members

        private string _apiKey;
        private string _customSearchEngineKey;
        private bool _isInitialized = false;
        private static readonly GoogleCustomSearchProvider instance = new GoogleCustomSearchProvider();
        private CustomsearchService gSearchService;

        #endregion

        /// <summary>
        /// Key required for executing google custom search api.
        /// If you do not have one, please create in google console project.
        /// </summary>
        public string ApiKey
        {
            get
            {
                return _apiKey;
            }

            set
            {
                _apiKey = value;
            }
        }

        /// <summary>
        /// This key refers to the custom search engine that you define. It is associated with a set of capabilities which can be modified in the engine settings.
        /// </summary>
        public string SearchEngineKey
        {
            get
            {
                return _customSearchEngineKey;
            }

            set
            {
                _customSearchEngineKey = value;
            }
        }

        #region Constructors
        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static GoogleCustomSearchProvider()
        {

        }

        private GoogleCustomSearchProvider()
        {
            
        }

        public bool Initialize(string ApiKey,string SearchEngineId)
        {
            _apiKey = ApiKey;
            _customSearchEngineKey = SearchEngineId;

            gSearchService = new CustomsearchService(new BaseClientService.Initializer { ApiKey = _apiKey });
            _isInitialized = true;
            return true;
        }

        public IList<Result> ExecuteSearch(string query,int numberOfResultsToBeReturned = 100)
        {

            IList<Result> completeResults = new List<Result>();

            try
            {
                if (_isInitialized)
                {
                    var listRequest = gSearchService.Cse.List(query);
                    listRequest.Cx = _customSearchEngineKey;

                    //listRequest.Num = 100; //setting this should return the exact number of results. currently seeing an issue with google api.

                    Console.WriteLine("Search started...");
                    IList<Result> paging = new List<Result>();

                    var count = 0;
                    int pageCount = numberOfResultsToBeReturned / 10;
                    while (paging != null)
                    {
                        listRequest.Start = count * 10 + 1;
                        paging = listRequest.Execute().Items;
                        if (paging != null)
                            completeResults = completeResults.Concat(paging).ToList();
                        
                        if (count == pageCount)
                            break;

                        count++;
                    }
                    Console.WriteLine("Search ended.");
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Could not execute the request due to exception : ");
                Console.WriteLine(ex.Message);
            }
           

            return completeResults;
         }

        #endregion

        #region static methods
        public static GoogleCustomSearchProvider Instance
        {
            get
            {
                return instance;
            }
        }
        #endregion

    }
}
