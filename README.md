# CustomSearch
Hello Guys,

This project is a demo of using C# for interaction with search engines.
This is still in draft version, able to talk to Google Search Engine.
For using this,

We need to have two things:

1) Google Api Key
2) Google Search Engine ID

We can get those by following below link:
http://hintdesk.com/2015/02/20/c-how-to-use-google-custom-search-api/

The project structure is built as follows:

1) A command object needs to be prepared that is uniform across the search engines.
2) A googlerankscommand is one specific type of command which interacts with google search engine for getting the ranks of a specific URL when specific keywords are used.
3) Currently we have GoogleSearchEngine class which is a singleton, all the queries to this class will be sequential.
4) In future this project can be extended to use BingSearchProvider.

Trial keys have limited capabilities :)



