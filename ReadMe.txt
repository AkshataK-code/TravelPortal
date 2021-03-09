Travel Portal performs following operations

1. request for /candidate returns :

{
 "name": “test”,
 "phone": “test”
}


2. request for /Location returns

It makes a GET call to http://ipstack.com/ API to  return the city that corresponds to caller's IP address


3. request for /Listings/2

where 2 is number of passengers

It filters out listings that don’t support the number of passengers.With the remaining
listings, it calculates the total price for the given number of passengers and returns the results sorted by total price (ascending order)

The code makes a GET call to search endpoint
https://jayridechallengeapi.azurewebsites.net/api/QuoteRequest to get the search data. It filters the data corresponding to number
of passengers, calculates total price and adds it to a linked list.It then sorts the linked list based on price.

It gives an appropriate message if number of passengers is less than or equal to zero or out of range.


==========================================================================================================

The project makes use of Controllers,Models and Services for Candidate, Location and PassengerListings.

It uses IHttpClientFactory by directly injecting the factory instance into the class that requires an HttpClient instance.
