using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchCommands
{
    /// <summary>
    /// Command for getting the Ranks within google results of a specific URL.
    /// </summary>
    public interface ISearchRanksCommand 
    {
        /// <summary>
        /// Keywords used for searching entire web
        /// </summary>
        string KeyWords
        {
            get;
            set;
        }

        /// <summary>
        /// URL to be searched within the results.
        /// </summary>
        string URLToSearch
        {
            get;
            set;
        }

        /// <summary>
        /// Number determining the parsing of the URL within the top results.For eg: 100, meaning search for top 100 results.
        /// </summary>
        int NumberOfResultsToSearchFor
        {
            get;
            set;
        }

        string Execute(int numberOfResultsToLookFor=100);
    }
}
