using Google.Apis.Customsearch.v1;
using Google.Apis.Customsearch.v1.Data;
using Google.Apis.Services;
using GoogleRanksProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomSearch
{
    class Program
    {
        static void Main(string[] args)
        {
            string searchKeyWords = string.Empty;
            string urlToSearchFor = string.Empty;
            string searchAgain = string.Empty;
            do
            {
                Console.WriteLine("Enter the search key words : ");
                searchKeyWords = Console.ReadLine();

                Console.WriteLine("Enter the URL : ");
                urlToSearchFor = Console.ReadLine();

                GoogleRanksCommand ranksCommand = new GoogleRanksCommand(searchKeyWords, urlToSearchFor);
                string result = ranksCommand.Execute();

                if (string.IsNullOrEmpty(result))
                {
                    if(result == null)
                    {
                        Console.WriteLine("Could not initiate the search!");
                    }
                    else
                    Console.WriteLine("Could not find any!");
                }
                else
                {
                    Console.WriteLine("Entered URL is found in : " + result);
                }

                Console.WriteLine("Do you wanna try again ? Press Y ");
                searchAgain = Console.ReadLine();

            }
            while (searchAgain.Equals("Y", StringComparison.OrdinalIgnoreCase));

            Console.WriteLine("Thanks for using our service!");
        }
    }
}
